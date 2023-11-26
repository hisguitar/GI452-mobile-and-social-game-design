using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionalTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void North()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(new Vector3(0,1,0), Vector3.up);
    }
    [Test]
    public void South()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(new Vector3(0, -1, 0), Vector3.down);
    }
}