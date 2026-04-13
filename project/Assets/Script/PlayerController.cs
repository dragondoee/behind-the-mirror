using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float _moveSpeed = 3.5f;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] private float _speedChangeRate = 10f;

    // Inputs 
    private int _forwardInput = 0;
    private int _backwardInput = 0;
    private int _leftInput = 0;
    private int _rightInput = 0;

    // Axes
    private float _verticalAxeInput = 0;
    private float _horizontalAxeInput = 0;

    // Animation
    private Animator _animator;
    private int _animIDSpeed;
    private float _animationBlend;
    private int _animIDSpellCast;
    private float _spellCastTiming = 0.8f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDSpellCast = Animator.StringToHash("SpellCast");
    }

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
        float moveCoef = 1;
        if ( _verticalAxeInput != 0 && _horizontalAxeInput != 0) moveCoef = 0.71f;

        Vector2 moveInput = new Vector2(_horizontalAxeInput * moveCoef , _verticalAxeInput * moveCoef);

        // Walking animation
        float targetSpeed = moveInput == Vector2.zero ? 0f : _moveSpeed;

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        if (_animator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
        }
        
        if (moveInput == Vector2.zero) return;

        // Player's rotation to face the direction of movement
        Vector3 fromRotation = transform.rotation.eulerAngles;
        float moveAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
        Vector3 toRotation = new Vector3(0f, moveAngle, 0f);
        transform.rotation = Quaternion.Slerp( Quaternion.Euler(fromRotation), Quaternion.Euler(toRotation), 0.1f );

        // Player's movement
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * _moveSpeed * Time.deltaTime;
        transform.position += move;
    }

    public void SpellCastAnimation()
    {
        if (_animator)
        {
            _animator.SetTrigger(_animIDSpellCast);
        }
    }

    public float GetDelaySpellCastAnimation() => _spellCastTiming;
}
