using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int VelocityX = Animator.StringToHash("VelocityX");
    private static readonly int Stopping = Animator.StringToHash("Stopping");

    private Rigidbody2D _body;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public float speed;
    public float acceleration;
    private bool _isStopping;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetFloat(VelocityX, Mathf.Abs(_body.linearVelocityX));
        _animator.SetBool(Stopping, _isStopping);
    }

    private void FixedUpdate()
    {
        var currentVelocityX = _body.linearVelocityX;
        var targetVelocityX = InputSystem.actions["Move"].ReadValue<Vector2>().x * speed;
        if (currentVelocityX > 0.0001f && targetVelocityX < -0.0001f || currentVelocityX < -0.0001f && targetVelocityX > 0.0001f)
        {
            _isStopping = true;
            if (targetVelocityX > 0.0001f) _spriteRenderer.flipX = false;
            if (targetVelocityX < -0.0001f) _spriteRenderer.flipX = true;
        }
        else
        {
            _isStopping = false;
            if (currentVelocityX > 0.0001f) _spriteRenderer.flipX = false;
            if (currentVelocityX < -0.0001f) _spriteRenderer.flipX = true;
        }

        var mass = _body.mass;
        var acc = Mathf.Clamp((targetVelocityX - currentVelocityX) / Time.fixedDeltaTime, -acceleration, acceleration);

        _body.AddForceX(mass * acc);
    }
    
}