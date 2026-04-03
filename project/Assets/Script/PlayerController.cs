using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _moveSpeed = 3.5f;

    private int _forwardInput = 0;
    private int _backwardInput = 0;
    private int _leftInput = 0;
    private int _rightInput = 0;

    private float _verticalAxeInput = 0;
    private float _horizontalAxeInput = 0;

    private void Update()
    {
        OnMove();
    }

    public void setMoveForward(int value)
    {
        _forwardInput = value;
        _verticalAxeInput = _forwardInput - _backwardInput;
    }

    public void setMoveBackward(int value)
    {
        _backwardInput = value;
        _verticalAxeInput = _forwardInput - _backwardInput;
    }

    public void setMoveLeft(int value)
    {
        _leftInput = value;
        _horizontalAxeInput = _rightInput - _leftInput;
    }

    public void setMoveRight(int value)
    {
        _rightInput = value;
        _horizontalAxeInput = _rightInput - _leftInput;
    }

    private void OnMove()
    {
        Vector2 moveInput;
        float moveCoef = 1;
        if ( _verticalAxeInput != 0 && _horizontalAxeInput != 0) moveCoef = 0.71f;
            moveInput = new Vector2(_horizontalAxeInput * moveCoef , _verticalAxeInput * moveCoef);

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
