using UnityEngine;
using System.Collections;
using System;

public class DestroyObject : MonoBehaviour
{
    private bool _canBeDestroyed = false;
    private GameObject _indicator;

    private PlayerController _playerController;

    void Awake()
    {
        _indicator = GameObject.FindGameObjectWithTag("Indicator").gameObject;
    }
    void Start()
    {
        _indicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = true;
            _indicator.SetActive(true);
            _playerController = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = false;
            _indicator.SetActive(false);
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
        _indicator.SetActive(false);
        Destroy(gameObject);
    }
}
