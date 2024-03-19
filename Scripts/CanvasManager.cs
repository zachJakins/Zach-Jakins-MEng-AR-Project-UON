using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;
using static SpecialCanvasController;

public enum CanvasType
{
    StartMenu,
    CameraMenu,
    MainMenu,
    SelectMenu,
    CustomiseMenu,
    TransformMenu,
    TransformOptionsMenu,
    OptionsMenu,
    MaterialsMenu,

}

public class CanvasManager : MonoBehaviour
{ 

    public List<CanvasController> canvasControllerList;
    public CanvasController lastActiveCanvas;


    void Awake()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();//gets all components with canvas controller component

        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));//turns them off since they must initially be active to be found.

        SwitchCanvas(CanvasType.StartMenu);
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if(lastActiveCanvas !=null) 
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else { Debug.LogWarning("Desired Canvas Not Found"); }
    }
    public void SwitchCanvas(CanvasType _canvastype, TransformType _transformtype, GameObject _object)//special case.
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _canvastype);
        if (desiredCanvas != null)
        {
            SpecialCanvasController controller = desiredCanvas.GetComponent<SpecialCanvasController>();
            controller.TypeOfTransform = _transformtype;
            controller.ComponentToTransform = _object;
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else { Debug.LogWarning("Desired Canvas Not Found"); }
    }

}
