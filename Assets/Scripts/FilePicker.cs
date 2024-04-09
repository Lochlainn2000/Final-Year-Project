using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilePicker : MonoBehaviour
{
    [SerializeField] RawImage image;

    public void LoadFile()
    {
        /*string FileType = NativeFilePicker.ConvertExtensionToFileType("png,jpg,jpeg");

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Picked file: " + FinalPath);
            }
            else
            {
                FinalPath = path;
                StartCoroutine("LoadTexture");
            }
        }, NativeFilePicker.ConvertExtensionToFileType("png,jpg,jpeg"));*/


        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Texture2D texture = NativeGallery.LoadImageAtPath(path);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + path);
                return;
            }

            image.gameObject.SetActive(true);
            image.texture = texture;
            string fileName = "processed_image_" + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
            //PhotoManager.SavePhoto(fileName, texture.GetRawTextureData());
            ARLoadTexture.setTexture(texture);
            UploadSceneButtonManager.setImageUploaded(true);
        });
    }


    Texture2D MakeTextureReadable(Texture2D texture)
    {
        // Create a copy of the original texture with the 'Read/Write Enabled' flag set to true
        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        newTexture.filterMode = texture.filterMode;
        newTexture.wrapMode = texture.wrapMode;
        newTexture.SetPixels(texture.GetPixels());
        newTexture.Apply(true);

        return newTexture;
    }
}

