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
        speed = Random.Range(0f,4f);
        if(direction == 2)
        {
            speed = -speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (col.IsTouchingLayers(LayerMask.GetMask("Triggers")))
        {
            speed = -speed;
        }
        if (col.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            rbPlayer.velocity += new Vector2(speed, rbPlayer.velocity.y);
        }
    }
    private void MovePlatforms()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

}
