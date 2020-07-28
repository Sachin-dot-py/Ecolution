using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    Left,
    Right
}

public class Axe : MonoBehaviour
{
    public Rigidbody2D rb;
    public float BulletSpeed = 20f; // Speed of bullet
    public Direction direction;
    // Start is called before the first frame update
    void Start()
    {
        if (direction == Direction.Left){
            rb.velocity = -transform.right * BulletSpeed; // Set the bullet in motion
        }
        else{
            rb.velocity = transform.right * BulletSpeed; // Set the bullet in motion
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Player"){
        Destroy(gameObject);
        }
    }
}
