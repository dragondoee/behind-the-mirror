using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private bool _canBeDestroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = false;
        }
    }

    public void DestroyObjectMethod()
    {
        Debug.Log("DestroyObjectMethod called");
        Debug.Log("canBeDestroyed: " + _canBeDestroyed);
        if (_canBeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
