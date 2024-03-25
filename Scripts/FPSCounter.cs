using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    /* Assign this script to any object in the Scene to display frames per second */

    public float updateInterval = 0.5f; //How often should the number update

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;

    string fileName = string.Empty;
    string fileName2 = string.Empty;

    TextMeshProUGUI[] presentTextMeshObjects;
    TextMeshProUGUI fpsText;

    FileWriter fileWriter;

    // Use this for initialization
    void Start()
    {
        fileName = Application.persistentDataPath + "/fpsData.txt";
        fileName2 = Application.persistentDataPath + "/performanceData.txt";

        fileWriter = FindObjectOfType<FileWriter>();

        fileWriter.writeStringToPath((DateTime.Now).ToString(),fileName);

        Debug.Log(fileName);
        timeleft = updateInterval;

        presentTextMeshObjects = FindObjectsOfType<TextMeshProUGUI>(true);//list all text mesh objects in the scene
        foreach (TextMeshProUGUI text in presentTextMeshObjects)
        {
            if (text.name == "FPS")//find specific
            {
                fpsText = text;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = (accum / frames);
            fps = (float)Math.Round(fps, 2);
            fpsText.text = fps.ToString() + "FPS";

            fileWriter.writeStringToPath(fps.ToString(), fileName);
            fileWriter.writeStringToPath(fps.ToString(), fileName2);

            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}