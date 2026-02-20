using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private bool canBeDestroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBeDestroyed = false;
        }
    }

    public void DestroyObjectMethod()
    {
        Debug.Log("DestroyObjectMethod called");
        Debug.Log("canBeDestroyed: " + canBeDestroyed);
        if (canBeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
