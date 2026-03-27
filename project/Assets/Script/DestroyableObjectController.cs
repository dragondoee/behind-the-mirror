using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private bool _canBeDestroyed = false;
    private GameObject _indicator;

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeDestroyed = false;
            _indicator.SetActive(false);
        }
    }

    public void DestroyObjectMethod()
    {
        Debug.Log("DestroyObjectMethod called");
        Debug.Log("canBeDestroyed: " + _canBeDestroyed);
        if (_canBeDestroyed)
        {
            Destroy(gameObject);
            _indicator.SetActive(false);
        }
    }
}
