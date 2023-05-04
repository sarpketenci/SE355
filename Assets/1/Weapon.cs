using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnpoint;
    public float speed;
    
    void Start()
    {
        StartCoroutine(Shoot(2f));
    }

    IEnumerator Shoot(float wait)
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnpoint.transform.position, spawnpoint.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * speed,ForceMode2D.Impulse);
            yield return new WaitForSeconds(wait);
        }
    }
}
