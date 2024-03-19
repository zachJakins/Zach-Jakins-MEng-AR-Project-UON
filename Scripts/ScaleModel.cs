using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleModel : MonoBehaviour
{

    Button button;
    SpecialCanvasController specialCanvasController;

    ScaleControls scaleControls;

    CanvasManager canvasManager;

    Controls controls;
    void Start()
    {
        button = GetComponent<Button>();
        canvasManager = FindObjectOfType<CanvasManager>();
        specialCanvasController = GetComponentInParent<SpecialCanvasController>();
        controls = FindObjectOfType<Controls>(true);
        scaleControls = FindObjectOfType<ScaleControls>(true);
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        controls.TurnAllOffControls();

        if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            ImportedObject foundObject = ImportedObject.FindOriginalObject();
            scaleControls.objectToScale = foundObject.transform.gameObject;
        }
        else if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            scaleControls.objectToScale = specialCanvasController.ComponentToTransform;
        }
        scaleControls.transform.gameObject.SetActive(true);//turn on the rotate controls
        canvasManager.SwitchCanvas(CanvasType.CameraMenu);
    }
}
