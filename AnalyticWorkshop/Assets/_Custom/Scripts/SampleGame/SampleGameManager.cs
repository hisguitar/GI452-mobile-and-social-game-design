using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;

public class SampleGameManager : MonoBehaviour
{
    public static SampleGameManager Instance;
    public TMP_Text scoreText;
    public TMP_Text timeText;

    public int currentScore=0;
    public CoinSampleGame coin;
    public Transform player;
    public GameObject boomEffect;
    public GameObject thorns;
    public FinishSampleGame finish;
    public TimeCounterSampleGame timeCounter;

    public GameObject gameoverPanel;
    public GameObject youwinPanel;

    public int numLevel = 1;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        currentScore = 0;
        gameoverPanel.gameObject.SetActive(true);
        boomEffect.transform.position = player.transform.position;
        boomEffect.SetActive(true);
        player.gameObject.SetActive(false);
        timeCounter.isGameOver = true;  
    }
    public void YouWin()
    {
        youwinPanel.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        thorns.gameObject.SetActive(false);
        coin.gameObject.SetActive(false);
        timeCounter.isGameOver = true;

        AnalyticsService.Instance.CustomData(
            "levelDied",
            new Dictionary<string, object>
            {
                {"userLevel", numLevel},
                {"userScore", currentScore},
                {"timeCount", timeCounter.GetCurrentTime},
                {"posX", player.transform.position.x},
                {"posY", player.transform.position.y}
            }
            );
        // Optional - You can call Events.Flush() to send the event
        // immediately else it will be cached and sent with the next
        // scheduled uplode within the next 60 seconds
        AnalyticsService.Instance.Flush();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void AddScore()
    {
        currentScore += 1;
        scoreText.text = currentScore.ToString("0");

        if(currentScore %3 == 0)
        {
            SpawnFinishLine();
        }
    }
    
    public void SpawnFinishLine()
    {
        finish.RandomSpawn();
    }

    // Start is called before the first frame update
    void Start()
    {
        coin.RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
