using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnAllControlsOff : MonoBehaviour
{
    Button button;
    Controls controls;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        controls = FindObjectOfType<Controls>(true);
        button.onClick.AddListener(OnButtonClick);

    }

    void OnButtonClick()
    {
        controls.TurnAllOffControls();
    }
}
