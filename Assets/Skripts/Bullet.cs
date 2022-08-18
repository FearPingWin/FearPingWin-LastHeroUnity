using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //projectile destruction upon impact with any object
    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
