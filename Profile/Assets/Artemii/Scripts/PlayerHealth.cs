using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHealth = 100;
    [SerializeField] float invincibilityTime = 0.5f;
    bool invincible = false;
    private void Start()
    {
    }
    
    private void DisableInvinciblity()
    {
        invincible = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (invincible == true)
            {
                return;
            }
            if (playerHealth > 0)
            {
                playerHealth -= 10;
                invincible = true;
                Invoke("DisableInvinciblity", invincibilityTime);
                Debug.Log(playerHealth);
            }
            else
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
    }
}
