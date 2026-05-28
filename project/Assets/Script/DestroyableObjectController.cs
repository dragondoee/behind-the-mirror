using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DestroyObject : MonoBehaviour
{
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string INDICATOR_TAG = "Indicator";
    private static readonly string DESTROY_ACTION = "Destroy";

    private bool _canBeDestroyed = false;
    private GameObject[] _indicators;
    private PlayerController _playerController;
    private InputAction _destroyAction;

    void Awake()
    {
        _destroyAction = InputSystem.actions.FindAction(DESTROY_ACTION);
        _indicators = GameObject.FindGameObjectsWithTag(INDICATOR_TAG);
    }

    void Start()
    {
        SetIndicatorsActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            _canBeDestroyed = true;
            SetIndicatorsActive(true);
            _playerController = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_destroyAction.IsPressed() && _canBeDestroyed)
        {
            DestroyObjectMethod();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            _canBeDestroyed = false;
            SetIndicatorsActive(false);
            _playerController = null;
        }
    }

    public void DestroyObjectMethod()
    {
        if (_playerController)
        {
            _playerController.SpellCastAnimation();
            StartCoroutine(WaitAndDestroy(_playerController.GetDelaySpellCastAnimation()));
            return;
        }
        else
        {
            DestroyNow();
        }
    }

    private IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyNow();
    }

    private void DestroyNow()
    {
        SetIndicatorsActive(false);
        Destroy(gameObject);
    }

    private void SetIndicatorsActive(bool active)
    {
        if (_indicators == null) return;
        foreach (GameObject indicator in _indicators)
        {
            if (indicator != null)
                indicator.SetActive(active);
        }
    }
}