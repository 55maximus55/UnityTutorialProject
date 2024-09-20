using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _body;
    private Animator _animator;
    
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("VelocityX", Mathf.Abs(_body.linearVelocityX));
    }

    private void FixedUpdate()
    {
        var currentVelocityX = _body.linearVelocityX;
        var targetVelocityX = InputSystem.actions["Move"].ReadValue<Vector2>().x * speed;

        var mass = _body.mass;
        var acc = Mathf.Clamp((targetVelocityX - currentVelocityX) / Time.fixedDeltaTime, -acceleration, acceleration);
        
        _body.AddForceX(mass * acc);
    }
    
}
