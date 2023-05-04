using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
     public float speed; 
        public float distance; 
        private bool movingUp = true; 
        private Vector3 startPosition; 
    
        void Start()
        {
            startPosition = transform.position; 
        }
    
        void Update()
        {
         
            if (movingUp)
            {
               
                transform.Translate(Vector3.up * speed * Time.deltaTime);
    
              
                if (transform.position.y >= startPosition.y + distance)
                {
                    movingUp = false; 
                }
            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                
                if (transform.position.y <= startPosition.y)
                {
                    movingUp = true; 
                }
            }
        }
}
