using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private float _offsetZ;
    private float _maxPositionZ;

    void Start()
    {
        _offsetZ = transform.position.z - _playerTransform.position.z;
        _maxPositionZ = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 
                    Mathf.Clamp(_playerTransform.position.z + _offsetZ, _maxPositionZ, float.MaxValue));
    }
}
