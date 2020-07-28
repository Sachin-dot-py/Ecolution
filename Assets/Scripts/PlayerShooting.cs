using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int Bullets = 5; // Bullets the player has
    public bool ContinousFiring = false; // Is continous firing by holding down fire button allowed?
    public Animator animator; // Animator of Player
    public Transform GunPoint; // Point where bullets are fired from
    public GameObject Bullet; // Prefab of bullet
    private AudioSource GunSound; // Sound of gun
    private Vector2 StartPosition; // Starting position for resetting later
    Rigidbody2D rb; // Rigidbody of Sprite
    Game game; // Instance of Game class

    IEnumerator ActivateContinousFiring(int duration){
        ContinousFiring = true;
        yield return new WaitForSeconds(duration);
        ContinousFiring = false;
    }

    void ContinousFiringPowerUp(int duration){
        // PowerUp
        StartCoroutine(ActivateContinousFiring(duration));
    }

    void CollectAmmo (int GainedBullets){
        Bullets += GainedBullets;
        // Play bullet gained sound
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody of Sprite
        game = GetComponent<Game>(); // Instance of Game class
        GunSound = GetComponent<AudioSource>();
        StartPosition = rb.position; // Set the variable for later
    }

    void Shoot()
    {
        animator.SetBool("Shooting", true);
        Instantiate(Bullet, GunPoint.position, GunPoint.rotation);
        animator.SetBool("Shooting", false);
        GunSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ContinousFiring || Bullets > 0){
            if (Input.GetKeyDown("f")){ // Once a click firing
                // Shoot!
                Shoot();
                // Play shooting sound
                if (!ContinousFiring){
                    Bullets -= 1;
                }
            }
        }
        else{
            // No bullets
            // Play no ammo sound
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spike"){
            game.Die();
        }
    }
}
