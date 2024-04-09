using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.IO;

public class MakeImage : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] RawImage image;
    [SerializeField] Slider loading;
    [SerializeField] Button generateButton;
    bool generated = false;
    string genName = "";

    public void generate()
    {
        generateButton.interactable = false;
        image.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        Thread getImageThread = new Thread(LoadingImage);
        getImageThread.Start();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (generated)
        {
            Texture2D texture = PhotoManager.LoadPhoto();
            texture.name = genName;
            HomeImageList.addKey(genName);
            HomeImageList.AddCachedImage(texture);
            image.gameObject.SetActive(true);
            image.texture = texture;
            loading.gameObject.SetActive(false);
            generateButton.interactable = true;
            ARLoadTexture.setTexture(texture);
            GenerateSceneButtonManager.setImageGenerated(true);
            generated = false;
        }
    }

    void LoadingImage()
    {
        genName = Client.ImageFromPrompt(input.text);
        generated = true;
        //move to update and check trigger
    }
}
