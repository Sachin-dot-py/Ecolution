using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public string PowerUpDescription = "Extra Life!";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            other.gameObject.SendMessage("AddLife", 1);
            other.gameObject.SendMessage("PowerUp", PowerUpDescription);
            Destroy(gameObject);
        }
    }
}
