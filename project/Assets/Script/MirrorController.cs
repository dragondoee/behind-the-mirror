using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

public class MirrorController : MonoBehaviour
{
    private bool _canBeActivated = false;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private AudioSource _mirrorAudioSource;

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

        StartCoroutine(WaitAndChangeLevel(_mirrorAudioSource.clip.length));
    }

    private IEnumerator WaitAndChangeLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        _sceneController.LoadNextLevel();
    }
}
