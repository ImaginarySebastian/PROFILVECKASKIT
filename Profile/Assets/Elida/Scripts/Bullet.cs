using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]Animator ani;
    Enemy ene;
    EnemyFlying fly;
    private void Start()
    {
        ene = FindObjectOfType<Enemy>();
        fly = FindObjectOfType<EnemyFlying>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ene.enemyDeath = true;
            fly.enemyDeath = true;
            ani.SetBool("Death", true);
        }


        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
