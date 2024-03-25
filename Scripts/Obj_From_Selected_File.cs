using Dummiesman;
using SimpleFileBrowser;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static SimpleFileBrowser.FileBrowser;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;
using System;

public class Obj_From_Selected_File : MonoBehaviour
{
    string objPath = string.Empty;
    string prevObjPath = string.Empty;
    GameObject loadedObject;

    string fileName = string.Empty;
    string fileName2= string.Empty;
    FileWriter fileWriter;

    TextMeshProUGUI[] presentTextMeshObjects;
    TextMeshProUGUI loadText;

    private void Start()
    {
        fileWriter = FindObjectOfType<FileWriter>();

        fileName = Application.persistentDataPath + "/loadTimeData.txt";
        fileName2 = Application.persistentDataPath + "/performanceData.txt";

        fileWriter.writeStringToPath((DateTime.Now).ToString(), fileName);
        fileWriter.writeStringToPath((DateTime.Now).ToString(), fileName2);


        presentTextMeshObjects = FindObjectsOfType<TextMeshProUGUI>(true);//list all text mesh objects in the scene

        foreach (TextMeshProUGUI text in presentTextMeshObjects)
        {
            if (text.name == "Load Time")//find specific
            {
                loadText = text;
                break;
            }
        }
    }

    void Update()
    {
        if (FileBrowser.Success && (FileBrowser.Result[0] != null))
        {
            //Must copy obj to a persistent data path such that it can be accessed by our android application due to the SAF
            string objPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0])); //creates an apppropriate path to copy our obj into our persistent data path
            ImportedObject importedObject = FindObjectOfType<ImportedObject>();

            //check if path changed
            if (prevObjPath == objPath && importedObject != null)
            {
                Debug.Log("No Change");
            }
            else
            {
                Debug.Log("Changed Obj");

                FileBrowserHelpers.CopyFile(FileBrowser.Result[0], objPath);//copies
                DebugText.Log(objPath);//write the path to debug text

                if (loadedObject != null)//if we already have an object, destory the previous
                {
                    Destroy(loadedObject);
                    DebugText.Log("Destroyed Previous Object " + objPath);
                }


                //Apose 3D Code --Defunct--

                /*#if UNITY_EDITOR//Use Aspose when in Unity Editor.
                Aspose.ThreeD.TrialException.SuppressTrialException = true;
                var toOBJ = new Aspose.ThreeD.Scene(objPath);
                toOBJ.Save(objPath, FileFormat.WavefrontOBJ);
                #endif*/


                //create obj loader
                OBJLoader objLoader = new();
                float currentTime = Time.realtimeSinceStartup;
                loadedObject = objLoader.Load(objPath);//load obj                  
                loadedObject.SetActive(true);
                loadedObject.AddComponent<ImportedObject>();//adds a component to our object so that we can edit it later.

                Vector3 newScale = new(1.0f, 1.0f, 1.0f);
                loadedObject.transform.localScale = newScale;

                Debug.Log("Set Scale to :" + loadedObject.transform.localScale);

                float loadTime = Time.realtimeSinceStartup - currentTime;

                loadText.text = loadTime.ToString()+"s";

                fileWriter.writeStringToPath((FileBrowser.Result[0] + " " + loadTime), fileName);
                fileWriter.writeStringToPath((FileBrowser.Result[0] + " " + loadTime), fileName2);

                //turns off controls when new object is loaded
                Controls cont = FindObjectOfType<Controls>(true);
                cont.TurnAllOffControls();

            }
            prevObjPath = objPath;//update previous path
        }
        else
        {
            Debug.Log("Cancelled Select/Reset Model");

        }
    }


}
