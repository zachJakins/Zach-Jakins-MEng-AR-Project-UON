using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SpecialCanvasController;

public class SpecialCanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvas;
    public TransformType TypeOfTransform;
    public GameObject ObjectToTransform;

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
        canvasManager.SwitchCanvas(desiredCanvas, TypeOfTransform, ObjectToTransform);
    }
}
