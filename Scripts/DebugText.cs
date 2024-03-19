using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public static void Log(string _debugText)//static so that i can use the function anywhere without a specific instance
    {
        DebugText[] DebugTexts = FindObjectsOfType<DebugText>(true);//find all debug texts in scene (including inactives)
        foreach (DebugText text in DebugTexts)//should technically be only 1 debug text in scene, but just incase
        {
            text.GetComponent<TextMeshProUGUI>().text = _debugText;//set their text to be the input
        }
        Debug.Log(_debugText);
    }
}
