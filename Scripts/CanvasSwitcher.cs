using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvas;

    CanvasManager canvasManager;
    Button menuButton;

    void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClick);
        canvasManager = FindObjectOfType<CanvasManager>();

    }


    void OnButtonClick()
    {
        canvasManager.SwitchCanvas(desiredCanvas);
    }
}
