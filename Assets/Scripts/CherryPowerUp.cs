using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPowerUp : MonoBehaviour
{
    public Animator animator;
    public string PowerUpDescription = "Unlimited Firing";
    public int PowerUpDuration = 10;
    public float CollectedAnimationDuration = 0.5f;
    private bool Collected;

    IEnumerator AnimationCoroutine(){
        animator.SetBool("Collected", true);
        yield return new WaitForSeconds(CollectedAnimationDuration); // Wait for collected animation to finish
        Destroy(gameObject); //Destroy the powerup
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
            other.gameObject.SendMessage("ContinousFiringPowerUp", PowerUpDuration);
            other.gameObject.SendMessage("PowerUp", PowerUpDescription);
            StartCoroutine(AnimationCoroutine());
        }

    }
}
