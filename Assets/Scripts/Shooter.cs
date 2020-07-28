using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public bool Enabled = true; // Is continous firing by holding down fire button allowed?
    public float Pause = 1f;
    private float PauseTime;
    public GameObject Bullet; // Prefab of bullet

    void Shoot()
    {
        Instantiate(Bullet, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled && Time.time > PauseTime){
            PauseTime = Time.time + Pause;
            Shoot();
            // Shooting sound play
        }   
    }
}
