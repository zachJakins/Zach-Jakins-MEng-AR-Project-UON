using Dummiesman;
using SimpleFileBrowser;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static SimpleFileBrowser.FileBrowser;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;

public class Obj_From_Selected_File : MonoBehaviour
{
    string objPath = string.Empty;
    string prevObjPath = string.Empty;
    GameObject loadedObject;

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


#if UNITY_EDITOR//Use Aspose when in Unity Editor.
                var toOBJ= new Aspose.ThreeD.Scene(objPath);
                toOBJ.Save(objPath, FileFormat.WavefrontOBJ);
#endif
                //create obj loader
                OBJLoader objLoader = new();
                loadedObject = objLoader.Load(objPath);//load obj                  
                loadedObject.SetActive(true);
                loadedObject.AddComponent<ImportedObject>();//adds a component to our object so that we can edit it later.

                Vector3 newScale = new(1.0f, 1.0f, 1.0f);
                loadedObject.transform.localScale = newScale;

                Debug.Log("Set Scale to :" + loadedObject.transform.localScale);

            }
            prevObjPath = objPath;//update previous path
        }
        else
        {
            Debug.Log("Cancelled Select/Reset Model");

        }
    }


}
