using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupMenuCanvas : MonoBehaviour {

    GraphicRaycaster gr;

    private void Start()
    {
        gr = GetComponent<GraphicRaycaster>();

        SetChildrenActive(false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;
#else
        if (0 < Input.touchCount
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 pos = Input.GetTouch(0).position;
#endif
            //Create the PointerEventData with null for the EventSystem
            PointerEventData ped = new PointerEventData(null);
            //Set required parameters, in this case, mouse position
            ped.position = pos;
            //Create list to receive all results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast it
            gr.Raycast(ped, results);

            if (results.Count == 0)
                SetChildrenActive(false);
        }
    }

    void SetChildrenActive(bool active)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }

}
