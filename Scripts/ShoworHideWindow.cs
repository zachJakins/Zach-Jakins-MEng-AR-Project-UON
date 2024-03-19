using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using SimpleFileBrowser;
using TMPro;
using Dummiesman;
using UnityEngine.XR.ARFoundation;

public class ShoworHideWindow : MonoBehaviour
{
    public Button FileButton;
    FileBrowser.OnSuccess SuccessfulOpen;
    FileBrowser.OnCancel SuccessfulCancel;
    FileBrowser.PickMode PickMode;

    private void Start()
    {
        DebugText.Log("Started");
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Obj Files", ".obj"), new FileBrowser.Filter("3D Model Formats", ".obj",".fbx", ".stl", ".3DS", ".glTF"));
        FileBrowser.SetDefaultFilter("Obj Files");

        if (FileBrowser.CheckPermission() == FileBrowser.Permission.Denied)
        {
            FileBrowser.RequestPermission();
            DebugText.Log("Requested Perms");

        }
        else if (FileBrowser.CheckPermission() == FileBrowser.Permission.Granted)
        {
            DebugText.Log("Perms Granted");
        }
        else
        {
            FileBrowser.RequestPermission();
            DebugText.Log("Requested Perms (2)");
        }
        Button fileb = FileButton.GetComponent<Button>();
        fileb.onClick.AddListener(TaskOnClick);
        Debug.Log("started");
    }
    void TaskOnClick()
    {
        Debug.Log("Click");

        
        if (FileBrowser.IsOpen)
        {
            FileBrowser.HideDialog();
        }
        else
        {
            FileBrowser.ShowLoadDialog(SuccessfulOpen, SuccessfulCancel, PickMode, false, null, null, "Select Object", "Load");
        
        }
        
    }

}
