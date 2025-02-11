using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject fireBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Quaternion means rotation and Quaternion.Identity is 000 for x,y,z rotations
            GameObject bullet = Instantiate(fireBullet, transform.position,Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x; 
        }
    }
}
