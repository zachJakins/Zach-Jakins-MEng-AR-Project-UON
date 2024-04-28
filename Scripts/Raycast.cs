using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public CanvasManager manager;

    public Camera ARCamera;
    public RaycastHit hit;

    public string raycastObjectName;
    public Vector3 raycastObjectBounds;
    public GameObject raycastObject;


    private ImportedObject importedObject;

    private void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && (manager.lastActiveCanvas.canvasType == CanvasType.CameraMenu))
            {
                Debug.Log(hit.collider.name.ToString());
                raycastObjectName = hit.collider.name;
                raycastObjectBounds = hit.collider.bounds.size;

                importedObject = ImportedObject.FindOriginalObject();
                for (int i = 0; i < importedObject.childrenCount; i++)
                {
                    if (importedObject.childrenName[i] == raycastObjectName)
                    {
                        raycastObject = importedObject.children[i];
                        break;
                    }
                }

                ClickedObjectDetails details = FindObjectOfType<ClickedObjectDetails>(true);
                details.gameObject.SetActive(true);

            }
        }
    }


}
