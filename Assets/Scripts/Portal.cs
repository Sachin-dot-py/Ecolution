using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int RequiredCoins = 2; // Coins required to unlock the portal
    public GameObject player;
    public Animator animator;
    private Game game;
    private bool Unlocked; // Is the portal unlocked?

    // Start is called before the first frame update
    void Start()
    {
        Unlocked = false;
        game = player.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.Coins == RequiredCoins){
                Unlocked = true;
                animator.SetBool("Unlocked", true);
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && Unlocked){
            // Do stuff
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
        }
    }
}
