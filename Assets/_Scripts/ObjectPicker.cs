using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour {

    public Camera MainCamera;

    public GameObject PickedObject;
    public PopupMenu popupMenu; 

    private int i = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if (PickedObject != hit.transform.gameObject)
            {
                if (null != popupMenu)
                    popupMenu.gameObject.SetActive(false);
                popupMenu = null;
                PickedObject = hit.transform.gameObject;
                popupMenu = PickedObject.GetComponent<Pickable>().popupMenu;
            }

            if (null != popupMenu 
                && !popupMenu.gameObject.activeSelf)
            {
                popupMenu.gameObject.SetActive(true);
            }

        }
        if (0 < Input.touchCount)
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if (PickedObject != hit.transform.gameObject)
            {
                if (null != popupMenu)
                    popupMenu.gameObject.SetActive(false);
                popupMenu = null;
                PickedObject = hit.transform.gameObject;
                popupMenu = PickedObject.GetComponent<Pickable>().popupMenu;
            }

            if (null != popupMenu
                && !popupMenu.gameObject.activeSelf)
            {
                popupMenu.gameObject.SetActive(true);
            }

        }
    }
}
