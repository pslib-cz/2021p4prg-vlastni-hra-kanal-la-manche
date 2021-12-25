using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlatformScript : MonoBehaviour
{
    public float speed;
    public float rightEdge;
    public float leftEdge;
    public GameManagerScript gm;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver == true)
        {
            return;
        }
        float horizontal = Input.GetAxis("HorizontalCoop");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftEdge)
        {
            transform.position = new Vector2(leftEdge, transform.position.y);
        }
        if(transform.position.x > rightEdge)
        {
            transform.position = new Vector2(rightEdge, transform.position.y);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("extraLife"))
        {
            audio.Play();
            gm.UpdateLives(1);
            Destroy(other.gameObject);   
        }
        if (other.CompareTag("extraScore"))
        {
            audio.Play();
            gm.UpdateScore(10);
            Destroy(other.gameObject);   
        }
    }
}
