using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwingObstacle : MonoBehaviour
{
    private Rigidbody2D rb;
   private float moveSpeed;
    public float leftAngle;
    public float rightAngle;
    public bool moving;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moving = false;
        moveSpeed = Random.Range(30f, 90f);
    }

    private void Update()
    {
        Move();
    }

    public void ChangeDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            moving = false;
            moveSpeed = Random.Range(30f, 90f);
        }

        if (transform.rotation.z < leftAngle)
        {
            moving = true;
            moveSpeed = Random.Range(30f, 90f);
        }
        
    }

    public void Move()
    {
        ChangeDir();
        if (moving)
        {
            rb.angularVelocity = moveSpeed;
        }
        if (!moving)
        {
            rb.angularVelocity = -1*moveSpeed;
        }
    }
}