using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleControls : MonoBehaviour
{
    public Button increaseScale, decreaseScale;
    public TMP_InputField scaleInput;
    float scaleValue;
    public TMP_Text currentScale;

    public GameObject objectToScale;
    Vector3 objectScale;

    void OnEnable()
    {
        increaseScale.onClick.AddListener(IncreaseSize);
        decreaseScale.onClick.AddListener(DecreaseSize);

        scaleInput.onValueChanged.AddListener(InputChanged);
        InputChanged(scaleInput.text);

        objectScale = objectToScale.transform.localScale;
        currentScale.text = Convert.ToString(objectScale[0]);

    }
    void OnDisable()
    {
        increaseScale.onClick.RemoveAllListeners();
        decreaseScale.onClick.RemoveAllListeners();
        scaleInput.onValueChanged.RemoveAllListeners();
    }

        void IncreaseSize()
    {
        Vector3 sizeIncrease = new(scaleValue, scaleValue, scaleValue);
        DebugText.Log(Convert.ToString(sizeIncrease[0]));
        objectToScale.transform.localScale = objectToScale.transform.localScale + sizeIncrease;
        objectScale = objectToScale.transform.localScale;
        currentScale.text = Convert.ToString(objectScale[0]);
    }
    void DecreaseSize()
    {
        Vector3 sizeDecrease = new(scaleValue, scaleValue, scaleValue);
        DebugText.Log(Convert.ToString(sizeDecrease[0]));
        objectToScale.transform.localScale = objectToScale.transform.localScale - sizeDecrease;
        objectScale = objectToScale.transform.localScale;
        currentScale.text = Convert.ToString(objectScale[0]);
    }

    void InputChanged(string _arg)
    {
        scaleValue = float.Parse(_arg);
    }

}
