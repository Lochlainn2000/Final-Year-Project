using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UploadSceneButtonManager : MonoBehaviour
{
    static bool imageUploaded = false;
    public void Accept()
    {
        if (imageUploaded)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Back()
    {

        SceneManager.LoadScene("Home");
    }
    public static void setImageUploaded(bool b)
    {
        imageUploaded = b;
    }
}
