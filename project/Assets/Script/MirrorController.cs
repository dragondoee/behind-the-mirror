using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

public class MirrorController : MonoBehaviour
{
    private bool _canBeActivated = false;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private AudioSource _mirrorAudioSource;
    [SerializeField] private GameObject _mirrorEffect;

    void Start()
    {
        if (_mirrorEffect)
        {
            _mirrorEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeActivated = false;
        }
    }

    public void ActivateMirrorMethod()
    {
        if (!_canBeActivated) return;

        if (_mirrorAudioSource)
        {
            _mirrorAudioSource.Play();
        }

        if (_mirrorEffect)
        {
            _mirrorEffect.SetActive(true);
        }

        StartCoroutine(WaitAndChangeLevel(_mirrorAudioSource ? _mirrorAudioSource.clip.length : 1f));
    }

    private IEnumerator WaitAndChangeLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        _sceneController.LoadNextLevel();
    }
}
