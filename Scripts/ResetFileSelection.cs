using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ResetFileSelection : MonoBehaviour
{
    public Button resetButton;
    void Start()
    {
        resetButton.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        if (FileBrowser.Success) {DebugText.Log("Destroyed Model: " + FileBrowserHelpers.GetFilename(FileBrowser.Result[0])); FileBrowser.Result[0] = null; }
        else { DebugText.Log("Destroyed Model"); }
        
        FindObjectOfType<Controls>(true).TurnAllOffControls();//turn off controls to prevent bugs
        Destroy(ImportedObject.FindOriginalObject());//destroys original object
    }
}
