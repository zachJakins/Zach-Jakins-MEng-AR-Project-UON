using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MaterialMenuController : MonoBehaviour
{
    SpecialCanvasController specialCanvasController;
    TextMeshProUGUI topText;

    SpecialCanvasController.TransformType typeOfTransform = SpecialCanvasController.TransformType.Unknown;
    GameObject componentToTransform;

    public Transform content;
    public GameObject materialButton;

    public List<Sprite> sprites;
    public List<string> shaderNames;
    public List<Shader> shaderList;

    private void OnEnable()
    {
        foreach (Transform child in content)
        {
            GameObject.Destroy(child.gameObject);
        }

        specialCanvasController = GetComponentInParent<SpecialCanvasController>();
        typeOfTransform = specialCanvasController.TypeOfTransform;
        componentToTransform = specialCanvasController.ComponentToTransform;

        SpecificText temp = GetComponentInChildren<SpecificText>();
        topText = temp.GetComponent<TextMeshProUGUI>();
        topText.text = "WORKED";

        if (typeOfTransform == SpecialCanvasController.TransformType.EntireModel)
        {
            componentToTransform = ImportedObject.FindOriginalObject().gameObject;
            topText.text = "Change Material: Entire Model";
            CreateMaterialButtons();
        }
        else if (typeOfTransform == SpecialCanvasController.TransformType.SpecificComponent)
        {
            topText.text = "Change Material: " + componentToTransform.name;
            CreateMaterialButtons();
        }
        else Debug.Log("Unknown Transform in Transform Options Needs Fixing");
    }
    private void OnDisable()
    {

    }

    private void CreateMaterialButtons()
    {
        if (shaderNames.Count == shaderList.Count && shaderNames.Count == sprites.Count)
        {
            for (int i = 0; i < shaderNames.Count; i++)
            {
                CreateMaterialButton(shaderNames[i], i, shaderList[i]);
            }
        }
        else Debug.Log("NOT ENOUGH SHADER NAMES/SHADERS/SPRITES");

    }

    private void CreateMaterialButton(string _str, int _count, Shader _shader)
    {
        GameObject materialButtonObject = Instantiate(materialButton, content);
        ChangeModelMaterial modelMaterial = materialButtonObject.transform.GetComponentInChildren<ChangeModelMaterial>();
        modelMaterial.buttonText.text = _str;
        modelMaterial.buttonImage.sprite = sprites[_count];
        modelMaterial.objectToTransform = componentToTransform;
        modelMaterial.materialShader = _shader;

    }
}
