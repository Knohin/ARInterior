/*
 * 클릭(혹은 터치) 를 이용해 Pickable객체를 선택한다.
 * 선택된 Pickable객체는 등록되있는 PopupMenu를 띄운다.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectPicker : MonoBehaviour {

    public Pickable PickableObject;

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
                PickableObject = hit.transform.GetComponent<Pickable>();
                if(PickableObject != null)
                {
                    PickableObject.popupMenu.transform.position = hit.point - ray.direction * 0.4f;
                    PickableObject.popupMenu.gameObject.SetActive(true);
                }
            }
        }
    }


}
