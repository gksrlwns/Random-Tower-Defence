using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsVector3
{
    public void SetVector3(string key, Vector3 v3)
    {
        PlayerPrefs.SetFloat(key + "x", v3.x);
        PlayerPrefs.SetFloat(key + "y", v3.y);
        PlayerPrefs.SetFloat(key + "z", v3.z);
    }

    public Vector3 GetVector3(string key)
    {
        Vector3 v3 = Vector3.zero;
        PlayerPrefs.GetFloat(key + "x");
        PlayerPrefs.GetFloat(key + "y");
        PlayerPrefs.GetFloat(key + "z");
        return v3;
    }
}
