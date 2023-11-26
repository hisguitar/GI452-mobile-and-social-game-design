using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounterSampleGame : MonoBehaviour
{
    public TMP_Text timeText;
    private float gameplayTimer=0;
    private int minutes;
    private int seconds;
    private string timer;
    public bool isGameOver;
    public int GetCurrentTime
    {
        get => (int)gameplayTimer;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            gameplayTimer += Time.unscaledDeltaTime;
            minutes = Mathf.FloorToInt(gameplayTimer / 60F);
            seconds = Mathf.FloorToInt(gameplayTimer - minutes * 60f);
            timer = string.Format("{0:00}:{1:00}", minutes, seconds);
            timeText.text = timer;
        }
        
       
    }
}
