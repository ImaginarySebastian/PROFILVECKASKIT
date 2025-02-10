using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.LoadSceneAfterDeath(); 
            }
            else
            {
                Debug.LogWarning("PlayerHealth-komponenten saknas på spelaren!");
            }
        }

     
        Destroy(gameObject);
    }
}