using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    // NOTE: You want our game? No, your access just got DENIED!

    public int Score = 0; // Score of player
    public int Coins = 0; // Number of coins collected by player
    public int Lives = 3; // Number of lives the player has
    public float RespawnInvicibilityTime = 2f;
    public bool RestartEnabled = true; // Is restart from checkpoint using 'r' key allowed?
    public bool GameIsPaused = false;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject YouDiedScreen;
    public TMPro.TextMeshProUGUI TimeBox;
    public TMPro.TextMeshProUGUI CoinsBox;
    public TMPro.TextMeshProUGUI LivesBox;
    public TMPro.TextMeshProUGUI PowerUpBox;
    public int PowerUpTextDuration = 2;
    public Vector2 StartPosition; // Starting position for resetting later
    private int TimeElapsed;
    private float InvincibilityTimer;
    Rigidbody2D rb; // Rigidbody of Sprite
    PlayerMovement pm;

    IEnumerator DieCoroutine(){
        pm.MoveAllowed = false;
        YouDiedScreen.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        yield return new WaitForSecondsRealtime(2); // Wait for some time
        Restart();
        YouDiedScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        InvincibilityTimer = Time.time + RespawnInvicibilityTime;
        yield return new WaitForSecondsRealtime(0.1f);
        pm.MoveAllowed = true;
    }

    IEnumerator PowerUpCoroutine(string PowerUpText){
        PowerUpBox.text = PowerUpText;
        yield return new WaitForSecondsRealtime(PowerUpTextDuration); // Wait for some time
        PowerUpBox.text = "";
    }

    void AddLife(){
        // Called by Heart powerup
        Lives += 1;
    }

    public void PowerUp(string PowerUpText){
        StartCoroutine(PowerUpCoroutine(PowerUpText));
    }

    public void Die(){
        // Player death screen
        Lives -= 1;
        if (Lives == 0){
            GameOver();
        }
        else{
            if (Time.time > InvincibilityTimer){
                if (!GameIsPaused){
                StartCoroutine(DieCoroutine());
                }
            }
        }
    }

    public void GameOver()
    {
        GameOverMenu.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void SetCheckpoint (Vector2 position){
        PowerUp("Set Checkpoint!");
        StartPosition = position;
    }

    void Restart(){
        // Restart at start of level or checkpoint if saved
        rb.position = StartPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody of Sprite
        pm = GetComponent<PlayerMovement>(); // Rigidbody of Sprite
        StartPosition = rb.position;
        // DontDestroyOnLoad(gameObject); // Don't destroy player on load of new scene.
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed = (int) Time.time;
        // TimeElapsed = (int) (Time.realtimeSinceStartup - PausedTime);
        TimeBox.text = TimeElapsed.ToString();
        CoinsBox.text = "Trees Planted: " + Coins;
        LivesBox.text = "Lives: " + Lives;
        if (Input.GetKey("r") && RestartEnabled){
            // Reset - Bring character back to start
            PowerUp("Restarted at Checkpoint");
            Restart();
        }
        if (Input.GetKeyDown("p") || Input.GetKeyDown("escape"))
        {
            // Pause game
            if (GameIsPaused){
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
                GameIsPaused = false;
            }
            else{
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
        }

        if (Input.GetKeyDown("l")){
            AddLife();
            PowerUp("Cheat Code :)");
        }

        if (Input.GetKeyDown("c")){
            Coins += 1;
            PowerUp("Cheat Code :)");
        }

    }

    // Called when level ends
    void OnDisable()
    {
        // PlayerPrefs.SetInt("lives", Lives);
    }

    // Called when level starts
    void OnEnable()
    {
        // Lives = PlayerPrefs.GetInt("lives");
        // Debug.Log(Lives);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Coin"){
            Coins += 1;
            PowerUp("Watered Tree");
            other.gameObject.SendMessage("PlantTree");
        }
        if (other.gameObject.tag == "Axe"){
            Die();
        }
    }
}
