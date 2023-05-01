using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
 
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.3f;
    public Vector2 bottomOffset1,bottomOffset2,bottomOffset3,bottomOffset4, rightOffset, leftOffset;

    void Update()
    {  
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset1, collisionRadius, groundLayer) ||Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset2, collisionRadius, groundLayer) ||Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset3, collisionRadius, groundLayer) ||Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset4, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) 
                 || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset1, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset3, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset4, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}

/*

public class Collision : MonoBehaviour
{
 
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.3f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Vector2 playersize;

    private void Awake()
    {
        playersize = GetComponent<BoxCollider2D>().size;
        bottomOffset = new Vector2(playersize.x, 0.05f);
    }

    void Update()
    {  
       // onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
       Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playersize.y + bottomOffset.y * 0.5f);
       onGround = Physics2D.OverlapBox(boxCenter,bottomOffset,0f,groundLayer) != null;
       onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) 
                 || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}


*/