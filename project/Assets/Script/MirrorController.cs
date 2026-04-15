using UnityEngine;
using System.Collections;
using System;

public class MirrorController : MonoBehaviour
{
    private bool _canBeActivated = false;
    [SerializeField] private SceneController _sceneController;

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

        StartCoroutine(WaitAndChangeLevel(1.0f));
    }

    private IEnumerator WaitAndChangeLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        _sceneController.LoadNextLevel();
    }
}
