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
    Bounds[] bounds;
    public Vector3[] componentMeasurements;
    public Vector3[] componentCentres;
    public bool Original;

    public ARTrackedImage trackedImage;

    private void Start()
    {
        TextMeshProUGUI[] presentTextMeshObjects = FindObjectsOfType<TextMeshProUGUI>();//list all text mesh objects in the scene

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
                Renderer partRend = children[i].GetComponentInChildren<Renderer>();
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

    private void OnDestroy()
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
    private void SelectedObjectDetails()
    {
        childrenCount = this.transform.childCount;//find all of its children
        Debug.Log(this);

        childrenName = new string[childrenCount];
        children = new GameObject[childrenCount];
        bounds = new Bounds[childrenCount];
        componentMeasurements = new Vector3[childrenCount];
        componentCentres = new Vector3[childrenCount];

        for (int i = 0; i < childrenCount; i++)
        {
            childrenName[i] = this.transform.GetChild(i).name;//find the object names

            //get each mesh boundaries sizes and centres
            bounds[i] = this.transform.GetChild(i).GetComponent<MeshFilter>().mesh.bounds;
            componentMeasurements[i] = bounds[i].size;
            componentCentres[i] = bounds[i].center;

            //make a new object with the same name as our current child
            GameObject spawnObject = new GameObject(childrenName[i]);
            //this new object then becomes a child of our wavefrontobject
            spawnObject.transform.parent = this.transform;
            //at which point we move it to the centre of our mesh
            spawnObject.transform.position = componentCentres[i];
            //then the component becomes child to this new transform meaning it will be rotated/scaled/translated about its bound centre
            this.transform.GetChild(i).parent = spawnObject.transform;
            //correct the sibling index since otherwise it would be at the bottom of our lsit
            spawnObject.transform.SetSiblingIndex(i);

            //children refers to the transform that is centred on our bounds
            children[i] = this.transform.GetChild(i).gameObject;

        }

    }
    private void populateDetailText()
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
        else Debug.Log("No AR Image in Scene");
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
