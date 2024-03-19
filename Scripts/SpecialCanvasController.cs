using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCanvasController : CanvasController
{
    public enum TransformType
    {
        EntireModel,
        SpecificComponent,
        Unknown
    }
    public TransformType TypeOfTransform;
    public GameObject ComponentToTransform;
}
