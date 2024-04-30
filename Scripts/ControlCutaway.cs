using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ControlCutaway : MonoBehaviour
{
    public TMP_InputField xInput;
    public TMP_InputField yInput;
    public TMP_InputField zInput;
    public Button directionButton;
    public GameObject objectToCutaway;
    public TMP_Dropdown dropDownScale;
    public Vector4 cutawayPlaneXYZ;
    public Button resetButton;


    public Toggle toggleCutPlane;

    private float scale = 1;


    // Start is called before the first frame update
    void Start()
    {

        directionButton.onClick.AddListener(ButtonPress);
        xInput.onValueChanged.AddListener(xValueChanged);
        yInput.onValueChanged.AddListener(yValueChanged);
        zInput.onValueChanged.AddListener(zValueChanged);
        dropDownScale.onValueChanged.AddListener(DropdownChanged);
        toggleCutPlane.onValueChanged.AddListener(ToggleCutPlaneChanged);
        resetButton.onClick.AddListener(ResetCutaway);

        cutawayPlaneXYZ.Set(float.Parse(xInput.text), float.Parse(yInput.text), float.Parse(zInput.text), 1);


    }

    public void OnEnable()
    {
        foreach (Renderer child in objectToCutaway.transform.GetComponentsInChildren<Renderer>(true))
        {
            child.material.SetVector("_CutPlanePos", cutawayPlaneXYZ * scale);
        }
    }

    public void Update()
    {
        ImportedObject.FindOriginalObject().planeCentre = cutawayPlaneXYZ * scale ;
        foreach (Renderer child in objectToCutaway.transform.GetComponentsInChildren<Renderer>(true))
        {
            child.material.SetVector("_CutPlanePos", cutawayPlaneXYZ * scale);
        }
    }

    private void ButtonPress()//whatever our current
    {
        float cutDir = objectToCutaway.GetComponentInChildren<Renderer>().material.GetFloat("_CutDir");//takes cut direction of first object in hierarchy
        foreach (Renderer child in objectToCutaway.transform.GetComponentsInChildren<Renderer>(true))
        {
            child.material.SetFloat("_CutDir", -cutDir);//sets all cut directions to be consistent
        }
        if (-cutDir > 0) directionButton.GetComponentInChildren<TMP_Text>().text = "+";//fix button text
        else directionButton.GetComponentInChildren<TMP_Text>().text = "-";

    }

    private void xValueChanged(string _val)
    {
        cutawayPlaneXYZ.x = float.Parse(xInput.text);
    }
    private void yValueChanged(string _val)
    {
        cutawayPlaneXYZ.y = float.Parse(yInput.text);
    }
    private void zValueChanged(string _val)
    {
        cutawayPlaneXYZ.z = float.Parse(zInput.text);
    }
    private void DropdownChanged(int _val)
    {
        switch (_val)
        {
            case 0://m
                scale = 1; break;
            case 1://cm
                scale = 1.0f / 100.0f; break;
            case 2://mm
                scale = 1.0f / 1000.0f; break;
        }
    }

    private void ToggleCutPlaneChanged(bool _val)
    {
        ImportedObject cutObject = ImportedObject.FindOriginalObject();
        cutObject.showPlane = _val;
        toggleCutPlane.isOn = _val;
    }
    private void ResetCutaway()
    {
        ImportedObject cutObject = ImportedObject.FindOriginalObject();
        foreach (Renderer child in cutObject.transform.GetComponentsInChildren<Renderer>(true))
        {
            child.material.SetVector("_CutPlanePos", new Vector3(0,0,0));
        }
        xInput.text = "0";
        yInput.text = "0";
        zInput.text = "0";
    }
}
