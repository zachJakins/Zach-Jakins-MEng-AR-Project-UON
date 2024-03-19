using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public void TurnAllOffControls()
    {
        foreach (Transform child in this.transform)//disables all controls
        {
            child.gameObject.SetActive(false);
        }
    }
}
