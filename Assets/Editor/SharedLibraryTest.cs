using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SharedLibraryTest {

	[Test]
	public void VectorLocationEqualSame() {
        var a = Vector3.one;
        var b = Vector3.one;
        var result =  SharedLibrary.VectorLocationEqual(a, b);
        Assert.IsTrue(result, "Failed to assert same Vector3");

        a = new Vector3(1, 1, 1);
        b = new Vector3(1, 0, 1);
        result = SharedLibrary.VectorLocationEqual(a, b);
        Assert.IsTrue(result, "Failed to ignore Y");

        a = new Vector3(1, 1, 1);
        b = new Vector3(1, 1, 0);
        result = SharedLibrary.VectorLocationEqual(a, b);
        Assert.IsFalse(result, "Did not recognise different X or Z");
    }

    [Test]
    public void CardinalDirectionBasicTest()
    {
        var N = SharedLibrary.North;
        var W = SharedLibrary.West;
        var S = SharedLibrary.South;
        var E = SharedLibrary.East;
        var Z = Vector3.zero;
        var result = Vector3.zero;

        result = SharedLibrary.CardinalDirection(N);
        Assert.AreEqual(N, result, "Failed to assert North");

        result = SharedLibrary.CardinalDirection(W);
        Assert.AreEqual(W, result, "Failed to assert West");

        result = SharedLibrary.CardinalDirection(S);
        Assert.AreEqual(S, result, "Failed to assert South");

        result = SharedLibrary.CardinalDirection(E);
        Assert.AreEqual(E, result, "Failed to assert East");

        result = SharedLibrary.CardinalDirection(Z, Vector3.zero);
        Assert.AreEqual(Z, result, "Failed to assert Zero");
    }

    [Test]
    public void CardinalDirectionAdvancedTest()
    {
        var N = SharedLibrary.North;
        var W = SharedLibrary.West;
        var S = SharedLibrary.South;
        var E = SharedLibrary.East;
        var result = Vector3.zero;

        result = SharedLibrary.CardinalDirection(new Vector3(-0.7f, 0f, 0.5f));
        Assert.AreEqual(S, result, "Failed to assert " + result +" as South");

        result = SharedLibrary.CardinalDirection(new Vector3(0.5f, 0f, -0.7f));
        Assert.AreEqual(E, result, "Failed to assert " + result + " as East");
    }

    [Test]
    public void CardinalToTrackDirectionTest()
    {
        var N = SharedLibrary.North;
        var trackNS = SharedLibrary.CardinalToTrackDirection(N);
        Assert.AreEqual(TrackDirection.NS, trackNS);
    }
}
