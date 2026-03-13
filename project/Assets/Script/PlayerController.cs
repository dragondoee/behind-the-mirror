using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction _moveAction;
    private float _moveSpeed = 5f;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        OnMove();
    }

    private void OnMove()
    {
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();

        if (moveInput == Vector2.zero) return;

        // Player's rotation to face the direction of movement
        Vector3 fromRotation = transform.rotation.eulerAngles;
        float moveAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
        Vector3 toRotation = new Vector3(0f, moveAngle, 0f);
        transform.rotation = Quaternion.Slerp( Quaternion.Euler(fromRotation), Quaternion.Euler(toRotation), 0.1f );

        // Player's movement
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * _moveSpeed * Time.deltaTime;
        transform.position += move;

        // TODO : add animation for movement
    }
}
