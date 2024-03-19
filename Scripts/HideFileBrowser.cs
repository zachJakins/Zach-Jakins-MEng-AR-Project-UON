using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideFileBrowser : MonoBehaviour
{
    Button FileButton;
    FileBrowser.OnSuccess SuccessfulOpen;
    FileBrowser.OnCancel SuccessfulCancel;
    FileBrowser.PickMode PickMode;

    private void OnEnable()
    {
        FileButton = GetComponent<Button>();
        FileButton.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        FileButton.onClick.RemoveListener(OnClick);
    }
    private void OnClick()
    {
        if (FileBrowser.IsOpen)
        {
            FileBrowser.HideDialog();
        }
    }

}
