using System;
using System.Collections.Generic;
using NUnit.Framework.Internal.Filters;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    
    private Rigidbody2D _body;
    
    public float speed = 3f;
    public bool moveRight = false;

    public ContactFilter2D contactFilter;
    public LayerMask groundLayer;
    public LayerMask defaultLayer;
    public Vector2 boxSize;
    public float castDistance;
    
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (IsCollides())
        {
            moveRight = !moveRight;
        }
        
        if (moveRight)
        {
            _body.linearVelocityX = speed;
        }
        else
        {
            _body.linearVelocityX = -speed;
        }
    }
    
    private bool IsCollides()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        var contacts = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, contactFilter, hits, castDistance);
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
    
}
