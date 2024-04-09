using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIImageHolder : MonoBehaviour
{
    string filename;

    public void SetFileName(string s)
    {
        filename = s;
    }
    public void RemoveImage()
    {
        HomeImageList.RemoveKey(filename);
        HomeImageList.RemoveCachedImage(((Texture2D) GetComponentInChildren<RawImage>().texture));
        Destroy(gameObject);
    }
    public void UseImage()
    {
        ARLoadTexture.setTexture(GetComponentInChildren<RawImage>().texture);
        SceneManager.LoadScene("SampleScene");
    }
}
