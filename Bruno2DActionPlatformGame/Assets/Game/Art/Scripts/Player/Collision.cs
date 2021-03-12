using System;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public float collisionRadius;
    public int side;

    public Color gizmoColor = Color.red;
    
    // Update is called once per frame
    void Update()
    {
        onWall = Physics2D.OverlapCircle((Vector2) transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2) transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2) transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2) transform.position + leftOffset, collisionRadius, groundLayer);
        side = onRightWall ? 1 : -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset,collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset,collisionRadius);
    }

    private void Start()
    {
        
    }
}
