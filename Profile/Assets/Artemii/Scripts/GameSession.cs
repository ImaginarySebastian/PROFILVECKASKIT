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

        switch (difficulty)
        {
            case "Easy":
                playerLives = 5;
                en.enemySpeed = 3f;
                sin.enemySpeed = 5f;
                fly.enemySpeed = 2f;
                break;
            case "Medium":
                playerLives = 3;
                en.enemySpeed = 5f;
                sin.enemySpeed = 7f;
                fly.enemySpeed = 4f;
                break;
            case "HardCore":
                playerLives = 1;
                en.enemySpeed = 10f;
                sin.enemySpeed = 13f;
                fly.enemySpeed = 7f;
                break;
            default:
                playerLives = 3;
                en.enemySpeed = 5f;
                sin.enemySpeed = 7f;
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
