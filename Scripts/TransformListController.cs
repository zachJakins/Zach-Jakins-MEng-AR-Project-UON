using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransformListController : MonoBehaviour
{
    public Transform ContentPanel;
    public GameObject ListItemPrefab;
    public GameObject EmptyListPrefab;

    ArrayList Components;

    void OnEnable()
    {
        OnChange();
    }
    void OnChange()
    { 
        ImportedObject foundObject = ImportedObject.FindOriginalObject();

        //if there actually is a model in the scene
        if (foundObject != null)
        {
            int childrenCount = foundObject.childrenCount;
            //deletes existing list elements when a new object is 
            foreach (Transform child in ContentPanel)
            {
                GameObject.Destroy(child.gameObject);
            }

            GameObject firstListElement = Instantiate(ListItemPrefab, ContentPanel) as GameObject;
            //instantiate new list item with component and populate component with name & associated component
            SpecialCanvasSwitcher firstController = firstListElement.GetComponent<SpecialCanvasSwitcher>();
            firstController.TypeOfTransform = SpecialCanvasController.TransformType.EntireModel;
            firstController.GetComponentInChildren<TextMeshProUGUI>().text = "Entire Model";
            firstController.ObjectToTransform = null;

            for (int i = 0; i < childrenCount; i++)
            {
                GameObject newListElement = Instantiate(ListItemPrefab, ContentPanel) as GameObject;
                //instantiate new list item with component and populate component with name & associated component
                SpecialCanvasSwitcher controller = newListElement.GetComponent<SpecialCanvasSwitcher>();
                controller.TypeOfTransform = SpecialCanvasController.TransformType.SpecificComponent;
                controller.GetComponentInChildren<TextMeshProUGUI>().text = foundObject.childrenName[i];
                controller.ObjectToTransform = foundObject.children[i];
            }
        }
        else//if there isnt a model in the scene
        {
            //deletes existing list elements when a new object is 
            foreach (Transform child in ContentPanel)
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject newComp = Instantiate(EmptyListPrefab, ContentPanel) as GameObject; //tells user why list is empty
        }

    }
}
