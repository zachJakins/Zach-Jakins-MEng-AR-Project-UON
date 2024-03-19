using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class ListItemController : MonoBehaviour
{
    public GameObject listItemComponent;
    public Toggle button;
    public Text Name;

    private void OnEnable()
    {
        button.onValueChanged.AddListener(ToggleEvent);
    }
    
    private void OnDisable()
    {
        button.onValueChanged.RemoveListener(ToggleEvent);
    }

    private void Start()
    {
        if (listItemComponent.activeSelf)
        {
            button.isOn = true;
        }
        else
        {
            button.isOn = false;
        }
    }


    void ToggleEvent(bool currentState)
    {
        listItemComponent.SetActive(currentState);
    }
}
