using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    private static readonly int Jump = Animator.StringToHash("Jump");
    private Animator _animator;
    private Rigidbody2D _body;

    public LayerMask groundLayer;
    public Vector2 boxSize;
    public float castDistance;
    
    public float jumpForce;
    public bool grounded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (InputSystem.actions["Jump"].IsPressed() && grounded)
        {
            _animator.SetBool(Jump, true);
            _body.linearVelocityY = jumpForce;
        }

        if (InputSystem.actions["Jump"].WasReleasedThisFrame())
        {
            if (_body.linearVelocityY > 0)
            {
                _body.linearVelocityY = jumpForce / 2;
            }
        }
    }
    
    private void FixedUpdate()
    {
        var onGround = IsGrounded();
        if (onGround && !grounded)
        {
            _animator.SetBool(Jump, false);
        }
        grounded = onGround;
    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
    
}
