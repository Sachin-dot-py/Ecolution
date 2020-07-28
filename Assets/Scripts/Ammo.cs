using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Animator animator; // Animator object
    public int GainedBullets = 5; // Bullets gained when player picks up an ammo refill;
    public float CollectedAnimationDuration = 0.5f;
    private bool Collected; // Is this ammo collected?


    IEnumerator AnimationCoroutine(){
            animator.SetBool("Collected", true);
            yield return new WaitForSeconds(CollectedAnimationDuration); // Wait for collected animation to finish
            Destroy(gameObject); //Destroy the ammo
        }

    // Start is called before the first frame update
    void Start()
    {
        Collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !Collected){
            Collected = true;
            other.gameObject.SendMessage("CollectAmmo", GainedBullets);
            other.gameObject.SendMessage("PowerUp", "Ammo Refill");
            StartCoroutine(AnimationCoroutine());
        }

    }
}
