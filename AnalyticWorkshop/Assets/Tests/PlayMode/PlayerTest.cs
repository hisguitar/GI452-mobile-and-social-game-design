using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MoveUp()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        var gameObject = new GameObject();
        var rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.drag = 3;
        rigid.transform.position = Vector3.zero;
        //for(int i = 0; i < 60; i++)
        {
            rigid.AddForce(gameObject.transform.position - (new Vector3(0, -1, 0) * 10), ForceMode2D.Impulse);

        }
        yield return new WaitForSeconds(0.2f);

        Debug.Log(gameObject.transform.position);
        Assert.Greater(rigid.transform.position.y, 0);
    }
}
