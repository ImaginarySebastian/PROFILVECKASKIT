using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;
    private void Start()
    {
        rb = GetComponent < Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    public void EnemyDead()
    {
        rb.velocity = Vector2.zero;
        ani.SetBool("Death", true);
        StartCoroutine(DestroyEnemy());
    }
    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
