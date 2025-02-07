using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperEnemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3f;
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] BoxCollider2D edge;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] CircleCollider2D explosiveZone;
    [SerializeField] float explosiveTime = 1f;
    bool isGrounded = true;
    GameObject player;
    Rigidbody2D rb;
    float jumpTime = 2f;
    float timeUntilNextJump = 4f;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            if (player != null)
            {
                float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
                rb.velocity = new Vector2(direction * enemySpeed, rb.velocity.y);
            }
        }
    }
    private IEnumerator Jump()
    {
        isGrounded = false;
        float directionOfJump = Mathf.Sign(player.transform.position.x - transform.position.x);

        rb.velocity = new Vector2(directionOfJump * enemySpeed, jumpSpeed);
        yield return new WaitForSeconds(jumpTime);

        yield return new WaitForSeconds(timeUntilNextJump);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (edge.IsTouchingLayers(LayerMask.GetMask("Hinder")))
        {
            Debug.Log("Det träffar");
            if (isGrounded)
            {
                StartCoroutine(Jump());
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(ExplosiveTimer());
    }
    private IEnumerator ExplosiveTimer()
    {
        if (explosiveZone.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            while (explosiveZone.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                Debug.Log("Staying at zone");
                yield return new WaitForSeconds(explosiveTime);
                playerHealth.LoadSceneAfterDeath();
            }
            Debug.Log("Boom!");
        }
    }
    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);
    }
}
