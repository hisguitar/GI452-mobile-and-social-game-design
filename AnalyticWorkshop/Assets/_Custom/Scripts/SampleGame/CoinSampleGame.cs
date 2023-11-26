using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinSampleGame : MonoBehaviour
{
    public bool isSpawnDone;

    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        isSpawnDone = false;
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawnDone)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one,Time.deltaTime);

            if(transform.localScale == Vector3.one)
            {
                isSpawnDone = true;
            }
        }
    }

    public void OnCollected()
    {
        SampleGameManager.Instance.AddScore();
        RandomSpawn();
    }

    public void RandomSpawn()
    {
        gameObject.SetActive(false);
        var xRandom = Random.Range(Screen.safeArea.xMin, Screen.safeArea.xMax);
        var yRandom = Random.Range(Screen.safeArea.yMin+150, Screen.safeArea.yMax-150);
        Debug.Log($"{Screen.safeArea.yMin} {Screen.safeArea.yMax}");
        var coinPos = Camera.main.ScreenToWorldPoint(new Vector3(xRandom, yRandom, transform.position.z));
        coinPos.z = transform.position.z;
        transform.position = coinPos;
        gameObject.SetActive(true);
    }
}
