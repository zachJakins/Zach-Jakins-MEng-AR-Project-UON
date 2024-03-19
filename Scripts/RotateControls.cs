using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RotateControls : MonoBehaviour
{

    private enum rotateDirection
    {
        plusX, minusX,
        plusY, minusY,
        plusZ, minusZ
    }

    public Button plusX, plusY, plusZ, minusX, minusY, minusZ;
    public TMP_InputField rotationAmountField;
    public float rotationAmount;
    public GameObject objectToMove;

    void OnEnable()
    {
        plusX.onClick.AddListener(delegate { Move(rotateDirection.plusX); });//+x
        minusX.onClick.AddListener(delegate { Move(rotateDirection.minusX); });//-x
        plusY.onClick.AddListener(delegate { Move(rotateDirection.plusY); });//+y
        minusY.onClick.AddListener(delegate { Move(rotateDirection.minusY); });//-y
        plusZ.onClick.AddListener(delegate { Move(rotateDirection.plusZ); });//+z
        minusZ.onClick.AddListener(delegate { Move(rotateDirection.minusZ); });//-z

        rotationAmountField.onValueChanged.AddListener(FieldAmountChanged);
        FieldAmountChanged(rotationAmountField.text);

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
        rotationAmountField.onValueChanged.RemoveAllListeners();
    }

    private void Move(rotateDirection _dir)
    {
        switch (_dir)
        {
            case (rotateDirection.plusX):
                objectToMove.transform.Rotate(rotationAmount, 0.0f, 0.0f,Space.World); break;
            case (rotateDirection.minusX):
                objectToMove.transform.Rotate(-(rotationAmount), 0.0f, 0.0f, Space.World); break;
            case (rotateDirection.plusY):
                objectToMove.transform.Rotate(0.0f, rotationAmount, 0.0f, Space.World); break;
            case (rotateDirection.minusY):
                objectToMove.transform.Rotate(0.0f, -(rotationAmount), 0.0f, Space.World); break;
            case (rotateDirection.plusZ):
                objectToMove.transform.Rotate(0.0f, 0.0f, rotationAmount, Space.World); break;
            case (rotateDirection.minusZ):
                objectToMove.transform.Rotate(0.0f, 0.0f, -(rotationAmount), Space.World); break;
        }



        Debug.Log("Rotated Object " + objectToMove.name + " " + rotationAmount + "deg");

    }

    private void FieldAmountChanged(string _val)
    {
        rotationAmount = float.Parse(_val);
        Debug.Log("Translate Val changed " + rotationAmount);
    }

}
