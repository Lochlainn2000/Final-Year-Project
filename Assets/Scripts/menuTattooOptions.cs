using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class menuTattooOptions : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] Slider size, rotation, transperency;
    [SerializeField] TextMeshProUGUI debugText;
    float maxSizeMultiplier = 0.1f*2f;
    // reference to tattoo that will be adjusted
    Transform obj;
    // Start is called before the first frame update
    void Start()
    {
        //preset slider values 
        size.value = 0.5f;
        rotation.value = 0.5f;
        transperency.value = 1f;
        panel.gameObject.SetActive(false);
    }
    public void setObj(Transform o)
    {
        //sets obj to null or tattoo
        obj = o;
        //if tattoo is touched show panel and set max tattoo size to twice its original size
        if(obj != null)
        {
            panel.gameObject.SetActive(true);
        }
        //if nothing touched panel vanishes
        else
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void ChangeSize()
    {
        //makes sure that tattoo is selected to avoid null ref error
        if(obj != null)
        {
            //gets original scale 
            Vector3 scale = obj.transform.localScale;
            //scales x and z according to size slider(doesnt scale y because tattoo is flat and not a cube)
            float clampedSize = Mathf.Clamp((size.value), 0.1f, 1);
            scale.x = maxSizeMultiplier * clampedSize;
            scale.z = maxSizeMultiplier * clampedSize;
            obj.transform.localScale = scale;
        }
    }
    public void ChangeRotation()
    {
        if(obj != null)
        {
            //get original rotation
            Vector3 rot = obj.transform.localRotation.eulerAngles;
            //slider is between 0 and 1 
            rot.y = 360 * rotation.value;
            obj.transform.localRotation = Quaternion.Euler(rot);
        }
    }
    public void ChangeTransperency()
    {
        if(obj != null)
        {
            //get current color 
            Color c = obj.GetComponent<Renderer>().material.color;
            //change alpha to transperency slider value(0 - 1)
            c.a = transperency.value;
            //set object material to new color
            obj.GetComponent<Renderer>().material.color = c;
        }
    }
}
