using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float BulletSpeed = 20f; // Speed of bullet

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * BulletSpeed; // Set the bullet in motione
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         Destroy(gameObject); // Destroy the enemy
    }
    void OnCollisonEnter2D(Collision2D other)
    {
        Destroy(gameObject); // Destroy the enemy
    }
}
