using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        using var watcher = new FileSystemWatcher(@"Assests/Resources/Generated");
        watcher.Created += OnCreate;
    }

    private static void OnCreate (object sender, FileSystemEventArgs e)
    {
        Debug.Log(e.FullPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
