using System.Collections;
using System.Collections.Generic;
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
    private ControlCutaway cutawayControls;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        cutawayControls = FindObjectOfType<ControlCutaway>(true);
    }
    void OnClick()
    {
        //if we pass the entire model
        foreach (Transform child in  objectToTransform.transform)
        {
            child.GetComponentInChildren<Renderer>(true).material.shader = materialShader;
        }
        //if we pass a specific component
        if(objectToTransform.GetComponentInChildren<Renderer>(true))
        {
            objectToTransform.GetComponentInChildren<Renderer>(true).material.shader = materialShader;
        }
        cutawayControls.transform.gameObject.SetActive(false);


    }
}