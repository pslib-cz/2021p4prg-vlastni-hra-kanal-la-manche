using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform platform;
    public Transform powerup;
    public Transform extraScore;
    public int speed;
    public GameManagerScript gm;
    public int scoreMultiplier;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = gm.ballSpeed;
        scoreMultiplier = gm.scoreMultiplier;
        audio = GetComponent<AudioSource>();
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
            audio.Play();
            gm.UpdateScore(other.gameObject.GetComponent<GlassScript>().points * scoreMultiplier);
            StartCoroutine(CheckLevelDelayed());
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        audio.Play();
        if(other.transform.CompareTag("brick"))
        {
            int randChance = Random.Range(1, 101);
            if (randChance < 25)
            {
                Instantiate(powerup, other.transform.position, other.transform.rotation);
            }
            if (randChance > 75)
            {
                Instantiate(extraScore, other.transform.position, other.transform.rotation);
            }

            gm.UpdateScore(other.gameObject.GetComponent<YellowScript>().points * scoreMultiplier);
            StartCoroutine(CheckLevelDelayed());
        }
        if(other.transform.CompareTag("redBrick"))
        {
            gm.UpdateScore(other.gameObject.GetComponent<RedScript>().points * scoreMultiplier);
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
