using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPicker : MonoBehaviour {

    public Pickable PickedObject;

    private void Awake()
    {
        this.tag = "MainCamera";
    }
    

    private void Update()
    {
        PickAndPopup();
    }

    private void PickAndPopup()
    {
        Vector2 pos;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            pos = Input.mousePosition;
#else
        if (0 < Input.touchCount
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;
            pos = Input.GetTouch(0).position;
#endif
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PickedObject = hit.transform.GetComponent<Pickable>();
                if(PickedObject != null)
                {
                    PickedObject.popupMenu.gameObject.SetActive(true);
                    PickedObject.popupMenu.transform.position = hit.point - ray.direction * 0.4f;
                }
            }
        }
    }


}
