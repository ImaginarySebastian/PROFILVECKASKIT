using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    Enemy en;
    SingleBulletEnemy sin;
    EnemyFlying fly;
    private void Awake()
    {
        int numGameSession = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        SetPlayerLivesByDifficulty();
        Debug.Log("Player Lives is " + playerLives);
        en = GetComponent<Enemy>();
    }
    private void SetPlayerLivesByDifficulty()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty","Medium");

        en = FindObjectOfType<Enemy>();
        sin = FindObjectOfType<SingleBulletEnemy>();
        fly = FindObjectOfType<EnemyFlying>();

        switch (difficulty)
        {
            case "Easy":
                playerLives = 5;
                if (en!=null)
                {
                    en.enemySpeed = 3f;
                }
                if(sin != null)
                {
                    sin.enemySpeed = 5f;
                }
                if (fly != null)
                {
                    fly.enemySpeed = 0.5f;
                }
                break;
            case "Medium":
                playerLives = 3;
                if (en != null)
                {
                    en.enemySpeed = 5f;
                }
                if (sin != null)
                {
                    sin.enemySpeed = 7f;
                }
                if (fly != null)
                {
                    fly.enemySpeed = 2f;
                }
                break;
            case "HardCore":
                playerLives = 1;
                if (en != null)
                {
                    en.enemySpeed = 10f;
                }
                if (sin != null)
                {
                    sin.enemySpeed = 13f;
                }
                if (fly != null)
                {
                    fly.enemySpeed = 5f;
                }
                en.enemySpeed = 10f;
                break;
            default:
                playerLives = 3;
                if (en != null)
                {
                    en.enemySpeed = 5f;
                }
                if (sin != null)
                {
                    sin.enemySpeed = 7f;
                }
                if (fly != null)
                {
                    fly.enemySpeed = 2f;
                }
                break;
        }
        Debug.Log("Difficulty:" + difficulty + "Player Lives set to " + playerLives);
    }
    public void TakeLife()
    {
        if(playerLives > 1)
        {
            Debug.Log("Player lives is " + playerLives);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            ResetGameSession();
        }
    }
    void ResetGameSession()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindFirstObjectByType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
        else
        {
            FindFirstObjectByType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
