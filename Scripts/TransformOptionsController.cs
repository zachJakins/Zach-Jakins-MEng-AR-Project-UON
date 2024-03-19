using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TransformOptionsController : MonoBehaviour
{
    SpecialCanvasController controller;
    SpecialCanvasController.TransformType TypeOfTransform = SpecialCanvasController.TransformType.Unknown;
    GameObject ComponentToTransform;
    TextMeshProUGUI topText;

    public Transform TransformOptionsContentPanel;
    public GameObject ListItemPrefab;
    public GameObject EmptyListPrefab;

    enum ButtonType
    {
        ChangeColour,
        ChangeMaterial,
        Translate,
        Rotate,
        TurnOffControls,
        Scale
    }

    private void OnEnable()
    {
        controller = GetComponentInParent<SpecialCanvasController>();
        TypeOfTransform = controller.TypeOfTransform;
        ComponentToTransform = controller.ComponentToTransform;

        SpecificText temp = GetComponentInChildren<SpecificText>();
        topText = temp.GetComponent<TextMeshProUGUI>();
        topText.text = "WORKED";

        //deletes existing list elements when a new object is 
        foreach (Transform child in TransformOptionsContentPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (TypeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            TotalModelTransform();
        }
        else if (TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            SpecificObjectTransform();
        }
        else Debug.Log("Unknown Transform in Transform Options Needs Fixing");
    }

    void TotalModelTransform()
    {
        Debug.Log("Total Model Transformation");
        topText.text = "Transform Entire Model";
        addButtonToList(ButtonType.ChangeColour);
        addButtonToList(ButtonType.ChangeMaterial);
        addButtonToList(ButtonType.Scale);
        addButtonToList(ButtonType.Translate);
        addButtonToList(ButtonType.Rotate);
        addButtonToList(ButtonType.TurnOffControls);
    }
    void SpecificObjectTransform()
    {
        Debug.Log("Specific Object Transformation");
        topText.text = "Transform Component: " + ComponentToTransform.name;
        addButtonToList(ButtonType.ChangeColour);
        addButtonToList(ButtonType.ChangeMaterial);
        addButtonToList(ButtonType.Translate);
        addButtonToList(ButtonType.Rotate);
        addButtonToList(ButtonType.TurnOffControls);

    }
    void addButtonToList(ButtonType _type)//function to simplify creation of various buttons
    {
        if(_type == ButtonType.ChangeColour)
        {
            GameObject listElement = Instantiate(ListItemPrefab, TransformOptionsContentPanel) as GameObject;//add a new button
            listElement.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Change Colour";//change what it says
            listElement.AddComponent<ChangeModelColour>();//add the appropriate script
        }
        else if(_type == ButtonType.ChangeMaterial)
        {
            GameObject listElement = Instantiate(ListItemPrefab, TransformOptionsContentPanel) as GameObject;
            listElement.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Change Material";
            SpecialCanvasSwitcher canvasSwitcher = listElement.AddComponent<SpecialCanvasSwitcher>();
            canvasSwitcher.desiredCanvas = CanvasType.MaterialsMenu;
            canvasSwitcher.TypeOfTransform = controller.TypeOfTransform;
            canvasSwitcher.ObjectToTransform = controller.ComponentToTransform;
        }
        else if (_type == ButtonType.Translate)
        {
            GameObject listElement = Instantiate(ListItemPrefab, TransformOptionsContentPanel) as GameObject;
            listElement.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Translate";
            listElement.AddComponent<TranslateModel>();
        }
        else if (_type == ButtonType.TurnOffControls)
        {
            GameObject listElement = Instantiate(ListItemPrefab, TransformOptionsContentPanel) as GameObject;
            listElement.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off Controls";
            listElement.AddComponent<TurnAllControlsOff>();
        }
        else if (_type == ButtonType.Rotate)
        {
            GameObject listElement = Instantiate(ListItemPrefab, TransformOptionsContentPanel) as GameObject;
            listElement.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Rotate";
            listElement.AddComponent<RotateModel>();
        }
        else if (_type == ButtonType.Scale)
        {
            GameObject listElement = Instantiate(ListItemPrefab, TransformOptionsContentPanel) as GameObject;
            listElement.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Scale";
            listElement.AddComponent<ScaleModel>();
        }
    }

}
