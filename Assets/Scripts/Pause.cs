using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject Player;
    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        game = Player.GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Quit()
    {
        Application.Quit();
    }

    void Resume()
    {
        game.PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        game.GameIsPaused = false;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        game.Score = 0;
        game.Coins = 0;
        game.Lives = 3;
        Time.timeScale = 1f;
        game.GameIsPaused = false;
    }

}
