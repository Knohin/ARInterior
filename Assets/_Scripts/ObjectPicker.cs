using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour {

    private Camera MainCamera;
    
    public Pickable PickedObject;
    private PopupMenu popupMenu;

    private bool touchDowned = false;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        Pickable newPick = hit.collider.GetComponent<Pickable>();
            //        if (null == newPick)
            //        { 
            //            if (null != popupMenu)
            //            {
            //                popupMenu.gameObject.SetActive(false);
            //                popupMenu = null;
            //            }
            //            PickedObject = null;
            //        }

            //        if (newPick != PickedObject)
            //        {
            //            if (null != popupMenu)
            //            {
            //                popupMenu.gameObject.SetActive(false);
            //                popupMenu = null;
            //            }
            //            PickedObject = newPick;
            //            popupMenu = PickedObject.popupMenu;
            //        }
            //    }
            //    else
            //    {
            //        if (null != PickedObject)
            //        {
            //            popupMenu.gameObject.SetActive(false);
            //            popupMenu = null;
            //            PickedObject = null;
            //        }
            //    }

            //    if (null != popupMenu 
            //        && !popupMenu.gameObject.activeSelf)
            //    {
            //    popupMenu.gameObject.SetActive(true);
            //    popupMenu.transform.position = hit.point - ray.direction * 0.8f + Vector3.up * 0.8f;
            //}
            

            Physics.Raycast(ray, out hit);
            if (PickedObject != hit.transform.gameObject)
            {
                if (null != popupMenu)
                    popupMenu.gameObject.SetActive(false);
                popupMenu = null;
                PickedObject = hit.collider.GetComponent<Pickable>();
                popupMenu = PickedObject.popupMenu;
            }

            if (null != popupMenu
                && !popupMenu.gameObject.activeSelf)
            {
                popupMenu.gameObject.SetActive(true);
                popupMenu.transform.position = hit.transform.position - ray.direction * 0.8f + Vector3.up * 0.8f;
            }

        }
        if (0 < Input.touchCount
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if (PickedObject != hit.transform.gameObject)
            {
                if (null != popupMenu)
                    popupMenu.gameObject.SetActive(false);
                popupMenu = null;
                PickedObject = hit.collider.GetComponent<Pickable>();
                popupMenu = PickedObject.popupMenu;
            }

            if (null != popupMenu
                && !popupMenu.gameObject.activeSelf)
            {
                popupMenu.gameObject.SetActive(true);
                popupMenu.transform.position = hit.transform.position - ray.direction * 0.8f + Vector3.up * 0.8f;
            }

        }
    }
}
