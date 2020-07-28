using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Vector3 TreeSize = new Vector3(1f, 1f, 1f);
    public Animator animator;
    public bool IsPlanted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlantTree()
    {
        gameObject.transform.localScale = TreeSize;
        animator.SetBool("Planted", true);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
