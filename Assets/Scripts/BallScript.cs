using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform platform;
    public int speed;
    public GameManagerScript gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver == true)
        {
            rb.velocity = Vector2.zero;
            transform.position = platform.position;
            inPlay = false;
            return;
        }
        if(inPlay == false)
        {
            transform.position = platform.position;
        }
        if(Input.GetButtonDown("Jump") && inPlay == false)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
            Debug.Log("spacebar");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("bottom"))
        {
            Debug.Log("Ball hit the bottom");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
        if (other.transform.CompareTag("NoCollisionBrick"))
        {
            gm.UpdateScore(other.gameObject.GetComponent<GlassScript>().points);
            StartCoroutine(CheckLevelDelayed());
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("brick"))
        {
            gm.UpdateScore(other.gameObject.GetComponent<YellowScript>().points);
            StartCoroutine(CheckLevelDelayed());
        }
        if(other.transform.CompareTag("redBrick"))
        {
            gm.UpdateScore(other.gameObject.GetComponent<RedScript>().points);
            StartCoroutine(CheckLevelDelayed());
        }
    }
    IEnumerator CheckLevelDelayed()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        gm.CheckLevel();
    }
}
