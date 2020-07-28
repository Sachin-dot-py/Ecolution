using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public int ScoreAwarded = 200; // Score awarded to player when killing enemy
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Something hit the enemy!
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject); // Destroy the bullet
            player.GetComponent<Game>().Score += ScoreAwarded; // Update score of player
        }
        Destroy(gameObject); // Destroy the enemy
    }

    void OnCollisonEnter2D(Collision2D other)
    {
        Destroy(gameObject); // Destroy the enemy
    }

}
