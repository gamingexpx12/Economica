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
}
