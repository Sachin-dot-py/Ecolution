using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector2 position;
    Game game;
    
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Player").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        position = (Vector2) transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            game.SetCheckpoint(position);
        }
    }
}
