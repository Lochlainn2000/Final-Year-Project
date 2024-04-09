using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GenerateSceneButtonManager : MonoBehaviour
{
    static bool imageGenerated = false;
   public void Accept()
    {
        if (imageGenerated)
        {
            PhotoManager.SavePhoto();
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Back()
    {

        SceneManager.LoadScene("Home");
    }
    public static void setImageGenerated(bool b)
    {
        imageGenerated = b;
    }
}
