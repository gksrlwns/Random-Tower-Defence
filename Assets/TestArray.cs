using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum array
{
    이게 =0,
    안돼,
    는거,
    야

}
public class TestArray : MonoBehaviour
{
    public int[] intArray;
    public Text CountArraytext;
    array array;
    // Start is called before the first frame update
    void Start()
    {
        intArray = new int[(int)array];
        for (int i = 0; i < intArray.Length; i++)
        {
            intArray[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CountArray()
    {
        intArray[1] = intArray[1] + 1;
        for (int i = 0; i < intArray.Length; i++)
        {
            print($"{intArray[i]}");
        }
        
        //CountArraytext.text = intArray[1].ToString();
    }
}
