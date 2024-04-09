using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLoadTexture : MonoBehaviour
{
    static Texture texture;
    void Start()
    {
        GetComponentInChildren<Renderer>().material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void setTexture(Texture text)
    {
        texture = text;
    }
}
