using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    Enemy en;
    SingleBulletEnemy sin;
    EnemyFlying fly;
    EnemyShoot shoot;
    PlayerShotting plaSho;
    Sanitymeter sanitymeter;
    private void Awake()
    {
        int numGameSession = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numGameSession > 1)
        {
            Debug.Log("Here is more then one Gamesessicion");
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
        sanitymeter = FindObjectOfType<Sanitymeter>();
    }
    private void SetPlayerLivesByDifficulty()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty","Medium");

        en = FindObjectOfType<Enemy>();
        sin = FindObjectOfType<SingleBulletEnemy>();
        fly = FindObjectOfType<EnemyFlying>();
        shoot = FindObjectOfType<EnemyShoot>();
        plaSho = FindObjectOfType<PlayerShotting>();

        switch (difficulty)
        {
            case "Easy":
                playerLives = 5;
                if (plaSho!=null)
                {
                    plaSho.BulletSpeed = 10;
                }
                if (shoot!=null)
                {
                    shoot.fireRate = 8;
                }
                if (en!=null)
                {
                    en.enemySpeed = 3f;
                    en.jumpSpeed = 5f;
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
                if (plaSho!=null)
                {
                    plaSho.BulletSpeed = 15;
                }
                if (shoot!=null)
                {
                    shoot.fireRate = 5;
                }
                if (en != null)
                {
                    en.enemySpeed = 5f;
                    en.jumpSpeed = 8f;
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
                if (plaSho!=null)
                {
                    plaSho.BulletSpeed = 50;
                }
                if (shoot != null)
                {
                    shoot.fireRate = 3;
                }
                if (en != null)
                {
                    en.enemySpeed = 10f;
                    en.jumpSpeed = 9f;
                }
                if (sin != null)
                {
                    sin.enemySpeed = 13f;
                }
                if (fly != null)
                {
                    fly.enemySpeed = 5f;
                }
                break;
            default:
                playerLives = 3;
                if (plaSho!=null)
                {
                    plaSho.BulletSpeed = 30;
                }
                if (shoot != null)
                {
                    shoot.fireRate = 5;
                }
                if (en != null)
                {
                    en.enemySpeed = 5f;
                    en.jumpSpeed = 8f;
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
        Debug.Log("Difficulty:" + difficulty + " Player Lives set to " + playerLives);
    }
    public void TakeLife()
    {
        Debug.Log("Minus life");
        playerLives--;
        HUDen hud = FindObjectOfType<HUDen>();
        if (hud != null)
        {
            hud.UpdateHearts(playerLives);
        }
        Debug.Log("Lives " + playerLives );
        if(playerLives > 0)
        {
            Debug.Log("Player lives is " + playerLives);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);

            StartCoroutine(ResetSanity());
        }
        else
        {
            ResetGameSession();
        }
    }
    private IEnumerator ResetSanity()
    {
        yield return new WaitForSeconds(0.1f); // V�nta tills scenen laddats in
        Sanitymeter sanity = FindObjectOfType<Sanitymeter>();
        if (sanity != null)
        {
            sanity.CurentSanity = sanity.MaxSanity;
            sanity.Walking = true;
            sanity.SanityMeter = sanity.gameObject.GetComponentInChildren<Slider>();
            sanity.OverLay = sanity.gameObject.GetComponentInChildren<Image>();
        }
    }
    void ResetGameSession()
    {
        ScenePersist persist = FindFirstObjectByType<ScenePersist>();

        persist.ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
