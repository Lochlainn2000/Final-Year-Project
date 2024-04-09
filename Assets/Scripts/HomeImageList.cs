using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeImageList : MonoBehaviour
{
    [SerializeField] Transform scrollContent, UIimage;
    static List<string> imageKeys;
    static List<Texture2D> cachedImages = new List<Texture2D>();
    static bool changed = true;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }
        if (cachedImages.Count != 0)
        {
            foreach (Texture2D t in cachedImages)
            {
                GameObject imageGO = Instantiate(UIimage.gameObject, scrollContent);
                RawImage imageComponent = imageGO.GetComponentInChildren<RawImage>();

                imageComponent.texture = t;
            }
            return;
        }
        cachedImages = new List<Texture2D>();
        loadKeys();
        foreach(string s in imageKeys)
        {
            Texture2D texture = PhotoManager.LoadPhoto(s);
            GameObject imageGO = Instantiate(UIimage.gameObject,scrollContent);
            imageGO.GetComponent<UIImageHolder>().SetFileName(s);
            RawImage imageComponent = imageGO.GetComponentInChildren<RawImage>();
            
            imageComponent.texture = texture;
            cachedImages.Add(texture);

        }
    }

    public static void loadKeys()
    {
        imageKeys = PlayerPrefsManager.LoadStringList("ImageKeys");
        Debug.Log("Key images = " + imageKeys.Count);
        changed = false;
    }
    public static void saveKeys()
    {
        PlayerPrefsManager.SaveStringList(imageKeys, "ImageKeys");
    }
    public static void addKey(string key)
    {
        imageKeys.Add(key);
        saveKeys();
        //changed = true;
    }
    public static void RemoveKey(string key)
    {
        imageKeys.Remove(key);
        PhotoManager.RemovePhoto(key);
        saveKeys();
        //changed = true;
    }
    public static void AddCachedImage(Texture2D texture)
    {
        cachedImages.Add(texture);
    }
    public static void RemoveCachedImage(Texture2D texture)
    {
        cachedImages.Remove(texture);
    }

}
