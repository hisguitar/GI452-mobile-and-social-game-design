using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSampleGame : MonoBehaviour
{
    public Rigidbody2D player;
    bool outOfBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            //print("exist a touch");

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //print("Touch begans");
                
            }
            if(Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                //print("Touch Stationary");
                var pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                pos.z = transform.position.z;
                player.AddForce(player.transform.position-pos);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //print("Touch Ended");
            }
        }
        var playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
        outOfBounds = !Screen.safeArea.Contains(playerPos);
        
        if (outOfBounds)
        {
            player.AddForce((Vector3.zero- player.transform.position)*.5f,ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            //print("Coin!");
            SampleGameManager.Instance.coin.OnCollected();
        }
        if (collision.CompareTag("Finish"))
        {
            //print("You Win!");
            SampleGameManager.Instance.YouWin();
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thorn"))
        {
            SampleGameManager.Instance.GameOver();
        }
    }
}
