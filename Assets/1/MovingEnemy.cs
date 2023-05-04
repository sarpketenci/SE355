using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public float speed;
    public float range;
     public float checkDelay;
    public LayerMask playerLayer;
    private Vector3[] directions = new Vector3[2];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    
    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();
        
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; 
        directions[1] = -transform.right * range; 
    }
    private void Stop()
    {
        destination = transform.position; 
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stop(); 
    }
}
