using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ListController : MonoBehaviour
{
    public Transform ContentPanel;
    public GameObject ListItemPrefab;
    public GameObject GenericListItem;
    public GameObject EmptyListPrefab;

    ArrayList Components;

    void OnEnable()
    {
        OnChange();
    }

    // Update is called once per frame
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

            GameObject selectAll = Instantiate(GenericListItem, ContentPanel) as GameObject;
            selectAll.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Select All";
            selectAll.AddComponent<SelectAllComponents>();

            GameObject deSelectAll = Instantiate(GenericListItem, ContentPanel) as GameObject;
            deSelectAll.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Deselect All";
            deSelectAll.AddComponent<DeselectAllComponents>();


            for (int i = 0; i < childrenCount; i++)
            {
                GameObject newComp = Instantiate(ListItemPrefab, ContentPanel) as GameObject;
                //instantiate new list item with component and populate component with name & associated component
                ListItemController controller = newComp.GetComponent<ListItemController>();
                controller.listItemComponent = foundObject.children[i];
                controller.Name.text = foundObject.childrenName[i];
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
