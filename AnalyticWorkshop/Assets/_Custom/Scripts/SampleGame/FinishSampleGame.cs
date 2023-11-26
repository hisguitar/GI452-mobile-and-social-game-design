using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishSampleGame : MonoBehaviour
{
    public TMP_Text finishText;
    public float currentCountDown = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCountDown -= Time.deltaTime;
        finishText.text = currentCountDown.ToString("0");
        if (currentCountDown <= 0)
        {
            AutoHide();
        }
    }

    public void RandomSpawn()
    {
        gameObject.SetActive(false);
        var xRandom = Random.value > .5f ? Screen.safeArea.xMin+75 : Screen.safeArea.xMax-75;
        var yRandom = Random.Range(Screen.safeArea.yMin + 150, Screen.safeArea.yMax - 150);
        Debug.Log($"{Screen.safeArea.yMin} {Screen.safeArea.yMax}");
        var finishPos = Camera.main.ScreenToWorldPoint(new Vector3(xRandom, yRandom, transform.position.z));
        finishPos.z = transform.position.z;
        transform.position = finishPos;
        gameObject.SetActive(true);       
    }

    public void AutoHide()
    {
        gameObject.SetActive(false);
        finishText.text = "3";
        currentCountDown = 3;
    }
}
