    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


//DEPRECATED BAD.
public class Find_Obj_Components : MonoBehaviour
{

    public TextMeshProUGUI text; //debug text


    // Update is called once per frame
    void Update()
    {
        var foundObject = FindObjectOfType<ARTrackedImage>();//find instantiated modle
        Debug.Log(foundObject);
        int childrenCount = foundObject.transform.childCount;//find all of its children
        string[] childName = new string[childrenCount];
        for (int i = 0; i < childrenCount; i++)
        {
            childName[i] = foundObject.transform.GetChild(i).name;
            Debug.Log(childName[i]);
        }
        string outputtedText = "";
        for (int i = 0; i < childrenCount; i++)
        {
            outputtedText += childName[i] + "\n";
        }
        text.text = outputtedText;

    }
}
