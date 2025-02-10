using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SingleBulletEnemy : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] BoxCollider2D edge;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float enemySpeed = 3f;
    private bool isGrounded = false;
    Rigidbody2D rb;
    private Transform player;
    private bool playerInRange = false;
    private bool haveShooted = false;
    float jumpTime = 2f;
    float timeUntilNextJump = 4f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AttackingCyckle());
    }
    private IEnumerator AttackingCyckle()
    {
        while (true)
        {
            Debug.Log("Checking if I should shoot...");
            if (playerInRange && !haveShooted)
            {
                Debug.Log("Shooting now!");
                Shoot();
                haveShooted = true;
            }
            yield return new WaitForSeconds(10f);
            haveShooted = false;
            yield return Run();
        }
    }
    void Update()
    {
        isGrounded = rb.IsTouching(groundFilter);
        if (isGrounded && edge.IsTouchingLayers(LayerMask.GetMask("Hinder")))
        {
            StartCoroutine(Jump());
        }
    }
    private IEnumerator Run()
    {
        if (isGrounded == true && player != null)
        {
            float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
            rb.velocity = new Vector2(direction * enemySpeed, rb.velocity.y);
        }
        yield return new WaitForSeconds(2f);
    }
    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;
        Debug.Log("Shooting at player!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null && player != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            playerInRange = true;
            Debug.Log("Player in range!");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}