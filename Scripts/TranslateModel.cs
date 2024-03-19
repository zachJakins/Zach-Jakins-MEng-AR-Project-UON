using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateModel : MonoBehaviour
{
    Button button;
    SpecialCanvasController specialCanvasController;

    TranslateControls translateControls;

    CanvasManager canvasManager;

    Controls controls;

    void Start()
    {
        button = GetComponent<Button>();
        canvasManager = FindObjectOfType<CanvasManager>();
        specialCanvasController = GetComponentInParent<SpecialCanvasController>();
        controls = FindObjectOfType<Controls>(true);
        translateControls = FindObjectOfType<TranslateControls>(true);
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        controls.TurnAllOffControls();

        if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            ImportedObject foundObject = ImportedObject.FindOriginalObject();
            translateControls.objectToMove = foundObject.transform.gameObject;
        }
        else if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            translateControls.objectToMove = specialCanvasController.ComponentToTransform;
        }
        translateControls.transform.gameObject.SetActive(true);//turn on the translate controls
        canvasManager.SwitchCanvas(CanvasType.CameraMenu);
    }
}
