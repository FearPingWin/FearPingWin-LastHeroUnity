using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;// projectile launch site
    public GameObject bulletPrefab;// projectile
    public float bulletForce=20f;//projectile speed
    // Update is called once per frame
    void Update()
    {   
        //when you press the left mouse button -shot
        if (Input.GetButtonDown("Fire1"))  {
            Shoot();
        }     
    }
    //player tank shooting
    void Shoot(){
        bulletPrefab.name="bullet_Player";
        GameObject bullet=Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Rigidbody2D rb=bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet,0.2f);// projectile range, removed after 0.2 seconds
    }
}
