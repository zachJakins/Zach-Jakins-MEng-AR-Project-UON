using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImportedObject : MonoBehaviour
{
    public TextMeshProUGUI detailText;
    public int childrenCount;
    public string[] childrenName;
    public GameObject[] children;
    public ARTrackedImage trackedImage;
    public bool Original;

    private void Start()
    {
        TextMeshProUGUI[] presentTextMeshObjects = FindObjectsOfType<TextMeshProUGUI>();//list all text mesh objects in the scene
        trackedImage = FindObjectOfType<ARTrackedImage>();

        ImportedObject[] allImportedObjects = FindObjectsOfType<ImportedObject>();
        if (allImportedObjects.Length < 2)
        {
            Original = true;
        }
        else
        {
            Original = false;
        }
        if (Original)
        {
            SelectedObjectDetails();

            foreach (TextMeshProUGUI text in presentTextMeshObjects)
            {
                if (text.name == "Object Detail Text")//find specific
                {
                    detailText = text;
                    populateDetailText();
                    break;
                }
            }

            //sets each component to random colour
            for (int i = 0; i < childrenCount; i++)
            {
                float colour = (i + 1.0f) * (1.0f / childrenCount);
                Shader partShader = Shader.Find("Standard");
                Renderer partRend = children[i].GetComponent<Renderer>();
                partRend.material.color = Random.ColorHSV(colour, colour, 1, 1, 1, 1);
                partRend.material.shader = partShader;


            }
        }

    }

    private void Update()
    {
        UpdateARModel();
        Debug.Log("Attempted Model Update");
    }

    public void OnDestroy()
    {
        if (Original)
        {
            detailText.alignment = TextAlignmentOptions.Top;
            detailText.text = "Currently No Model";
            ImportedObject[] allImportedObjects = FindObjectsOfType<ImportedObject>();

            //ensures that all instantiations are destroyed via one single command
            foreach (ImportedObject importedObject in allImportedObjects)
            {
                Destroy(importedObject.gameObject);
            }
        }

    }
    public void SelectedObjectDetails()
    {
        childrenCount = this.transform.childCount;//find all of its children
        Debug.Log(this);
        childrenName = new string[childrenCount];
        children = new GameObject[childrenCount];
        for (int i = 0; i < childrenCount; i++)
        {
            childrenName[i] = this.transform.GetChild(i).name;
            children[i] = this.transform.GetChild(i).gameObject;
            Debug.Log(childrenName[i]);
        }

    }
    public void populateDetailText()
    {
        detailText.alignment = TextAlignmentOptions.TopLeft;
        string outputtedText = "Current Object Details:\n" +
                                "File Name:\n\t\t" + FileBrowserHelpers.GetFilename(FileBrowser.Result[0]) +
                                "\nNumber of Components:\n\t\t" + childrenCount.ToString() + "\nComponent Names:\n";
        for (int i = 0; i < childrenCount; i++)
        {
            outputtedText += "\t\t" + childrenName[i].ToString() + "\n";
        }
        detailText.text = outputtedText;
    }

    public void UpdateARModel()
    {
        ARTrackedImage trackedImage = null;
        trackedImage = FindAnyObjectByType<ARTrackedImage>();
        if (trackedImage != null)
        {
            foreach (Transform child in trackedImage.transform)//destroys whatever was under the ARTrackedImage and places a new instantiation under
            {
                GameObject.Destroy(child.gameObject);
            }
            ImportedObject instantiatedObject = Instantiate(this, trackedImage.transform);
            Debug.Log("Transform Pos: " + instantiatedObject.transform.position);
        }
        else DebugText.Log("No AR Image in Scene");
    }
    public static ImportedObject FindOriginalObject()//Finds the original object and returns it
    {
        //finds our model in the world space
        ImportedObject[] foundObjects = FindObjectsOfType<ImportedObject>();//finds all instances of imported object (both the created one and the ARTrackedImage)
        ImportedObject foundObject = null;

        foreach (ImportedObject obj in foundObjects) //finds the original model
        {
            if (obj.Original == true)
            {
                foundObject = obj;
                break;
            }
        }
        return foundObject;
    }
}
