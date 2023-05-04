using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3[] cameraPositions;
    public float transitionSpeed;

    private int currentLevel = 0;
    private Vector3 desiredPosition;

    private void Update()
    {
       
       if (player.transform.position.x > -10 && player.transform.position.x <=10f)
        {
            currentLevel = 0;
        }
        else if (player.transform.position.x > 10f && player.transform.position.x <=30f)
        {
            currentLevel = 1;
        }
        else if (player.transform.position.x > 30f && player.transform.position.x <=50f)
        {
            currentLevel = 2;
        }
        else if (player.transform.position.x > 50f && player.transform.position.x <=70f)
        {
            currentLevel = 3;
        }
        else if (player.transform.position.x > 70f && player.transform.position.x <=90f)
        {
            currentLevel = 4;
        }
      
        desiredPosition = cameraPositions[currentLevel];
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, transitionSpeed * Time.deltaTime);
    }
}
