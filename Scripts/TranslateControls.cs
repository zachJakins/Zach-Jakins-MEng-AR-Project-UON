using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TranslateControls : MonoBehaviour
{

    private enum translateDirection
    {
        plusX, minusX,
        plusY, minusY,
        plusZ, minusZ
    }

    public Button plusX, plusY, plusZ, minusX, minusY, minusZ;
    public TMP_InputField translationAmountField;
    public float translationAmount;
    public TMP_Dropdown translationScaleDropdown;
    public float translationScale;
    public GameObject objectToMove;

     void OnEnable()
    {
        plusX.onClick.AddListener(delegate { Move(translateDirection.plusX); }) ;//+x
        minusX.onClick.AddListener(delegate { Move(translateDirection.minusX); }) ;//-x
        plusY.onClick.AddListener(delegate { Move(translateDirection.plusY); }) ;//+y
        minusY.onClick.AddListener(delegate { Move(translateDirection.minusY); }) ;//-y
        plusZ.onClick.AddListener(delegate { Move(translateDirection.plusZ); });//+z
        minusZ.onClick.AddListener(delegate { Move(translateDirection.minusZ); });//-z

        translationAmountField.onValueChanged.AddListener(FieldAmountChanged);
        FieldAmountChanged(translationAmountField.text);

        translationScaleDropdown.onValueChanged.AddListener(DropdownChanged);
        DropdownChanged(translationScaleDropdown.value);
    }

    private void OnDisable()
    {
        //remove all listeners on disable.
        plusX.onClick.RemoveAllListeners();
        plusY.onClick.RemoveAllListeners();
        plusZ.onClick.RemoveAllListeners();
        minusX.onClick.RemoveAllListeners();
        minusY.onClick.RemoveAllListeners();
        minusZ.onClick.RemoveAllListeners();
        translationAmountField.onValueChanged.RemoveAllListeners();
        translationScaleDropdown.onValueChanged.RemoveAllListeners();
    }

    private void Move(translateDirection _dir) 
    {
        switch(_dir)
        {
            case(translateDirection.plusX):
                objectToMove.transform.Translate(translationAmount * translationScale, 0.0f, 0.0f,Space.World); break;
            case (translateDirection.minusX):
                objectToMove.transform.Translate(-(translationAmount * translationScale), 0.0f, 0.0f, Space.World); break;
            case (translateDirection.plusY):
                objectToMove.transform.Translate(0.0f, translationAmount * translationScale, 0.0f, Space.World); break;
            case (translateDirection.minusY):
                objectToMove.transform.Translate(0.0f, -(translationAmount * translationScale), 0.0f, Space.World); break;
            case (translateDirection.plusZ):
                objectToMove.transform.Translate(0.0f, 0.0f, translationAmount * translationScale, Space.World); break;
            case (translateDirection.minusZ):
                objectToMove.transform.Translate(0.0f, 0.0f, -(translationAmount * translationScale), Space.World); break;
        }
        
        
        
        Debug.Log("Moved Object "+objectToMove.name+"Distance: "+translationAmount*translationScale+"m");

    }

    private void FieldAmountChanged(string _val)
    {
        translationAmount = float.Parse(_val);
        Debug.Log("Translate Val changed " + translationAmount);
    }

    private void DropdownChanged(int _val)
    {
        switch (_val)
        {
            case 0://m
                translationScale = 1; break;
            case 1://cm
                translationScale = 1.0f/100.0f; break;
            case 2://mm
                translationScale = 1.0f / 1000.0f; break;
        }
        Debug.Log("Scale changed " + translationScale);
    }
}
