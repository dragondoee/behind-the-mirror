using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Player
    [Header("Player")]
    private InputAction _moveAction;
    private static readonly string MOVE_ACTION = "Move";
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float _moveSpeed = 3.5f;

    [Tooltip("Acceleration and deceleration")]
    [SerializeField] private float _speedChangeRate = 10f;

    // Animation
    private Animator _animator;

    private int _animIDSpeed;
    private float _animationBlend;
    private static readonly string SPEED_ANIMATION = "Speed";

    private int _animIDSpellCast;
    private float _spellCastTiming = 0.8f;
    private static readonly string SPELLCAST_ANIMATION = "SpellCast";

    // Sounds
    [Header("Sounds")]
    [Tooltip("Audio source for the spell cast sound effect")]
    [SerializeField] private AudioSource _spellCastAudioSource;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction(MOVE_ACTION);
        _animator = GetComponent<Animator>();
        _animIDSpeed = Animator.StringToHash(SPEED_ANIMATION);
        _animIDSpellCast = Animator.StringToHash(SPELLCAST_ANIMATION);
    }

    private void Update()
    {
        OnMove();
    }

    private void OnMove()
    {
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();

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
            StartCoroutine(StopSpellCastAnimationAfterDelay(_spellCastTiming));
        }
        if (_spellCastAudioSource)
        {
            _spellCastAudioSource.Play();
        }
    }

    public IEnumerator StopSpellCastAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_animator)
        {
            _animator.ResetTrigger(_animIDSpellCast);
        }
    }

    public float GetDelaySpellCastAnimation() => _spellCastTiming;
}
