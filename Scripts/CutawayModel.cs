using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutawayModel : MonoBehaviour
{
    Button button;
    SpecialCanvasController specialCanvasController;

    ControlCutaway cutawayControls;

    CanvasManager canvasManager;

    Controls controls;

    void Start()
    {
        button = GetComponent<Button>();
        canvasManager = FindObjectOfType<CanvasManager>();
        specialCanvasController = GetComponentInParent<SpecialCanvasController>();
        controls = FindObjectOfType<Controls>(true);
        cutawayControls = FindObjectOfType<ControlCutaway>(true);
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        controls.TurnAllOffControls();

        if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            ImportedObject foundObject = ImportedObject.FindOriginalObject();
            foreach (Renderer rend in foundObject.transform.GetComponentsInChildren<Renderer>(true))
            {
                rend.material.shader = Shader.Find("Cutaway Shader");
            }

            cutawayControls.objectToCutaway = foundObject.transform.gameObject;
        }
        else if (specialCanvasController.TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            cutawayControls.objectToCutaway = specialCanvasController.ComponentToTransform;
            foreach (Renderer rend in specialCanvasController.ComponentToTransform.transform.GetComponentsInChildren<Renderer>(true))
            {
                rend.material.shader = Shader.Find("Cutaway Shader");
            }
        }
        cutawayControls.transform.gameObject.SetActive(true);//turn on the cutaway controls
        canvasManager.SwitchCanvas(CanvasType.CameraMenu);
    }
}
