using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class getImage : MonoBehaviour
{
    [SerializeField] string prompt;
    // Start is called before the first frame update
    void Start()
    {
        ImageFromPrompt(prompt);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string ImageFromPrompt (string prompt)
    {
        UnityEngine.Debug.Log(1);
        var psi = new ProcessStartInfo();
        UnityEngine.Debug.Log(2);
        psi.FileName = @"C:\Users\User\AppData\Local\Programs\Python\Python312\python.exe";
        UnityEngine.Debug.Log(3);

        var script = "Assets/Scripts/python/ImageGen.py";
        prompt = prompt.Replace(' ', '_');

        String name = "processed_image_" + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

        psi.Arguments = $"\"{script}\" \"{prompt}\" \"{name}\"";

        UnityEngine.Debug.Log(4);

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        UnityEngine.Debug.Log(5);
        var errors = "";
        var results = "";

        using (var process = Process.Start(psi))
        {
            UnityEngine.Debug.Log(6);
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();
            UnityEngine.Debug.Log(7);
        }
        UnityEngine.Debug.Log("ERRORS:");
        UnityEngine.Debug.Log(errors);
        UnityEngine.Debug.Log("");
        UnityEngine.Debug.Log("Results:");
        UnityEngine.Debug.Log(results);

        return name;
    }
   
}
