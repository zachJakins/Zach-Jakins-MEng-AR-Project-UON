using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateModel : MonoBehaviour
{
    Button button;
    SpecialCanvasController specialCanvasController;

    RotateControls rotateControls;

    CanvasManager canvasManager;

    Controls controls;

    void Start()
    {
        button = GetComponent<Button>();
        canvasManager = FindObjectOfType<CanvasManager>();
        specialCanvasController = GetComponentInParent<SpecialCanvasController>();
        controls = FindObjectOfType<Controls>(true);
        rotateControls = FindObjectOfType<RotateControls>(true);
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        controls.TurnAllOffControls();

        if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            ImportedObject foundObject = ImportedObject.FindOriginalObject();
            rotateControls.objectToMove = foundObject.transform.gameObject;
        }
        else if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            rotateControls.objectToMove = specialCanvasController.ComponentToTransform;
        }
        rotateControls.transform.gameObject.SetActive(true);//turn on the rotate controls
        canvasManager.SwitchCanvas(CanvasType.CameraMenu);
    }
}
