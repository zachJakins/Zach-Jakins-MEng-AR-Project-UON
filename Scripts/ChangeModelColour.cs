using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeModelColour : MonoBehaviour
{
    Button button;
    public FlexibleColorPicker colorPicker;
    FlexibleColorPicker colorPickerInstance;
    SpecialCanvasController controller;

    void Start()
    {
        controller = GetComponentInParent<SpecialCanvasController>();
        colorPicker = Resources.Load<FlexibleColorPicker>("CustomFCP");
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        
    }
    void OnButtonClick()
    {

       colorPickerInstance = Instantiate(colorPicker, controller.transform);
       colorPickerInstance.onColorChange.AddListener(colourChange);
        if (controller.TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            colorPickerInstance.color = controller.ComponentToTransform.GetComponent<Renderer>().material.color;//set it so that the colour picker starts as the colour of the specific component.
        }
    }
    void colourChange(Color _color)
    {
        Debug.Log("Color Change");
        if (controller.TypeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            controller.ComponentToTransform.GetComponent<Renderer>().material.color = colorPickerInstance.color;//will replace the specific colour of the specific component
        }
        else if (controller.TypeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            //finds our model in the world space  
            ImportedObject foundObject = ImportedObject.FindOriginalObject();

            //colours every single child
            foreach(GameObject child in foundObject.children)
            {
                child.GetComponent<Renderer>().material.color = colorPickerInstance.color;
            }
        }
        else Debug.Log("Unknown Model In Colour Changer");

    }
}
