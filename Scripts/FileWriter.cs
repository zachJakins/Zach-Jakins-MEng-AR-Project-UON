using SimpleFileBrowser;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileWriter : MonoBehaviour
{
    List<string> paths = new List<string>();
    List<string> contents = new List<string>();

    //add new path and string to our buffer
    public void writeStringToPath(string _str, string path)
    {
        paths.Add(path);
        contents.Add(_str);
    }

    //synchronously write to the files so that their arent any timing issues
    private void Update()
    {
        for (int i =0; i<paths.Count; i++)
        {
            using (StreamWriter sw = File.AppendText(paths[i]))//writes to the file
            {
                sw.WriteLine(contents[i]);
            } 
        }
        //emptys the lists
        paths.Clear();
        contents.Clear();

    }

}
