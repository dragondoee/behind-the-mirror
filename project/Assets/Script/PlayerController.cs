using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction m_moveAction;
    private float m_moveSpeed = 5f;

    private void Start()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        onMove();
    }

    private void onMove()
    {
        Vector2 moveInput = m_moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * m_moveSpeed * Time.deltaTime;
        transform.position += move;
    }
}
