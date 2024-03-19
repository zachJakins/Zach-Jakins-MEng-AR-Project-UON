using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class ChangeModelMaterial : MonoBehaviour
{
    Button button;
    public TextMeshProUGUI buttonText;
    public Image buttonImage;
    public GameObject objectToTransform;
    public Shader materialShader;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        //if we pass the entire model
        foreach (Transform child in  objectToTransform.transform)
        {
            child.GetComponent<Renderer>().material.shader = materialShader;
        }
        //if we pass a specific component
        if(objectToTransform.GetComponent<Renderer>())
        {
            objectToTransform.GetComponent<Renderer>().material.shader = materialShader;
        }
        
    }
}