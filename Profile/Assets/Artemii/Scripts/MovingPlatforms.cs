using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public GameObject Player;
    Collider2D col;
    int direction = 0;
    float speed = 0f;
    Rigidbody2D rb;
    Rigidbody2D rbPlayer;
    private bool Onplatform = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        RandomValues();
        rbPlayer = Player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MovePlatforms();
    }
    private void RandomValues()
    {
        direction = Random.Range(1,3);
        speed = 2f;
        if(direction == 2)
        {
            speed = -speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Onplatform = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Onplatform && collision.CompareTag("Player"))
        {
            rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);

            rbPlayer.gravityScale = 0;


        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Onplatform = false;
            
            rbPlayer.gravityScale = 1;
        }
    }

    void MovePlatforms()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}

