using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;


//DEPRACTED UNUSED
public class SelectedObjectDetails : MonoBehaviour
{
    public TextMeshProUGUI detailText;

    ImportedObject importedObject;
    private void Update()
    {
        importedObject = FindObjectOfType<ImportedObject>();
        if (importedObject != null)
        {
            detailText.alignment = TextAlignmentOptions.TopLeft;
            Debug.Log(importedObject);
            int childrenCount = importedObject.transform.childCount;//find all of its children
            string[] childName = new string[childrenCount];
            for (int i = 0; i < childrenCount; i++)
            {
                childName[i] = importedObject.transform.GetChild(i).name;
                Debug.Log(childName[i]);
            }
            string outputtedText = "Current Object Details:\n" +
                "File Name:\n\t\t" + FileBrowserHelpers.GetFilename(FileBrowser.Result[0]) +
                "\nNumber of Components:\n\t\t" + childrenCount.ToString() + "\nComponent Names:\n";

            for (int i = 0; i < childrenCount; i++)
            {
                outputtedText += "\t\t" + childName[i].ToString() + "\n";
            }
            detailText.text = outputtedText;
        }
        else {
            detailText.alignment = TextAlignmentOptions.Top;
            detailText.text = "Currently No Model";

        }

    }
}

