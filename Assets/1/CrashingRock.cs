using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashingRock : MonoBehaviour
{
    public float upSpeed;
    public float downSpeed;
    public Transform up;
    public Transform down;
    public bool hit;
   
    void Update()
    {
        if (transform.position.y >= up.position.y )
        {
            hit = true;
        }
        if (transform.position.y <= down.position.y )
        {
            hit = false;
        }

        if (hit)
        {
            transform.position = Vector2.MoveTowards(transform.position, down.position, downSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, up.position, upSpeed * Time.deltaTime);
        }
    }
}
