using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickedObjectDetails : MonoBehaviour
{
    public Button dismissButton;
    public TMP_Text detailText;
    public Toggle toggleObject;

    private Raycast ray;
    

    void Start()
    {
        ray = FindFirstObjectByType<Raycast>();//find raycast
        dismissButton.onClick.AddListener(Dismiss);//setup events
        toggleObject.onValueChanged.AddListener(ToggleObject);
    }
    private void Dismiss()
    {
        this.gameObject.SetActive(false);
    }
    private void ToggleObject(bool Toggle)
    {
        ray.raycastObject.SetActive(Toggle);
    }

    void Update()
    {
        toggleObject.isOn = ray.raycastObject.activeSelf;

        string objectName = ray.raycastObjectName;
        Vector3 objectBounds = ray.raycastObjectBounds;


        string newText =
            @"Object Name: " + objectName + @"

Object Size (m): " + objectBounds.x.ToString("F3") + ", " + objectBounds.y.ToString("F3") + ", " + objectBounds.z.ToString("F3") + @"
Object Size (cm): " + (objectBounds.x * 100).ToString("F2") + ", " + (objectBounds.y * 100).ToString("F2") + ", " + (objectBounds.z * 100).ToString("F2") + @"
Object Size (mm): " + (objectBounds.x * 1000).ToString("F1") + ", " + (objectBounds.y * 1000).ToString("F1") + ", " + (objectBounds.z * 1000).ToString("F1");

            detailText.text = newText;

    }
}
