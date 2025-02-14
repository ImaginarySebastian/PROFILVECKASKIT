using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator ani;
    
    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        ani.SetBool("Shott", true);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyDeath ne = collision.gameObject.GetComponent<EnemyDeath>();
            ne.EnemyDead();
        }
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
