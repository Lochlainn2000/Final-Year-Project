using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneButtonManager : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}
