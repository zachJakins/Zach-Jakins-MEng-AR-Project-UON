using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeselectAllComponents : MonoBehaviour
{
    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        CanvasManager canvasManager = FindObjectOfType<CanvasManager>();
        ImportedObject foundObject = ImportedObject.FindOriginalObject();
        foreach (Transform child in foundObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        canvasManager.SwitchCanvas(CanvasType.CustomiseMenu);
    }
}

