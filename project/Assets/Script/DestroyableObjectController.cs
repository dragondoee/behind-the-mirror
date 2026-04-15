using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour
{
    private bool _canBeDestroyed = false;
    private GameObject[] _indicators;
    private PlayerController _playerController;

    void Awake()
    {
        _indicators = GameObject.FindGameObjectsWithTag("Indicator");
    }

    void Start()
    {
        SetIndicatorsActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = true;
            SetIndicatorsActive(true);
            _playerController = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = false;
            SetIndicatorsActive(false);
            _playerController = null;
        }
    }

    public void DestroyObjectMethod()
    {
        if (!_canBeDestroyed) return;

        if (_playerController)
        {
            _playerController.SpellCastAnimation();
            StartCoroutine(WaitAndDestroy(_playerController.GetDelaySpellCastAnimation()));
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

    // Méthode helper pour activer/désactiver tous les indicators
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