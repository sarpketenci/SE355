using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasertrap : MonoBehaviour
{
    public GameObject[] lasers;
    void Start() {
        StartCoroutine(MainLoop());
    }

    private IEnumerator MainLoop() {
        while (true)
        {
            yield return StartCoroutine(On());
            yield return StartCoroutine(Off());
        }
       
    }
    

    IEnumerator On()
    {
        {
            foreach (GameObject la in lasers)
            {
                la.SetActive(true);
                yield return new WaitForSeconds (0.5f);
            }
        }
    }
    IEnumerator Off()
    {
        {
            foreach (GameObject la in lasers)
            {
                la.SetActive(false);
                yield return new WaitForSeconds (0.5f);
            }
        }
    }

}
