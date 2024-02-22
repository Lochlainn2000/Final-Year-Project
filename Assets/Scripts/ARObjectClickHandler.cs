using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectClickHandler : MonoBehaviour
{
    //Reference to camera in the scene
    [SerializeField] Camera ARcamera;
    //used for debuggin purposes
    [SerializeField] TextMeshProUGUI debugText;

    void Update()
    {
        // Check for screen touches
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Check if the touch phase is "Ended" and not on UI component  
            if (touch.phase == TouchPhase.Ended && !ClickedOnUi())
            {
                //Make ray out of camera from touch position
                Ray ray = ARcamera.ScreenPointToRay(touch.position);

                //get list of things ray hit
                RaycastHit[] hits = Physics.RaycastAll(ray, 100.0F);

                //get first object hit from list of hits that is not UI
                Transform hit = GetNonUi(hits);

                //setting object in tattoo menu script to know whihc obj the menu is altering
                GetComponent<menuTattooOptions>().setObj(hit);
            }
        }
    }

    // Gets the first non UI hit from list or returns null
    Transform GetNonUi(RaycastHit[] hits)
    {
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.gameObject.layer != LayerMask.NameToLayer("UI"))
            {
                return hit.transform;
            }
        }
        return null;
    }
    private bool ClickedOnUi()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.GetTouch(0).position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        // return results.Count > 0;
        foreach (var item in results)
        {
            if (item.gameObject.CompareTag("UI"))
            {
                debugText.text = "T";
                return true;
            }
        }
        debugText.text = "F";
        return false;
    }
}
