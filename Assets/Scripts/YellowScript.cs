using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowScript : MonoBehaviour
{
    private Animator anim;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        anim.Play("DestroyedYellow");
        Destroy(gameObject, 0.33f);
    }
}
