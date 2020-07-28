using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Videos : MonoBehaviour
{
    public VideoPlayer video;
    
    void Start()
    {
        video.loopPointReached += Continue;
    }
    void Continue(VideoPlayer player)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
    }

    void Update()
    {
        if (Input.GetKeyDown("s")){ // SKIP
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
        }
    }
}