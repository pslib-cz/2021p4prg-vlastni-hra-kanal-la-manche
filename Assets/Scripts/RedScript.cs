using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScript : MonoBehaviour
{
    private Animator anim;
    private int lives;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lives = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (lives == 0)
        {
            anim.Play("IdleYellow");    
            lives++;
        }
        else
        {
            if (lives == 1)
            {
                anim.Play("DestroyedYellow");
                Destroy(gameObject, 0.33f);
            }   
        }
    }
}
