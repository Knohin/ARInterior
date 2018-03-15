/*
 * 설명 : 
 *      이 스크립트를 PopupMenu가 있는 캔버스에 추가한다. 그러면
 *      클릭(혹은 터치)으로 PopupMenu가 선택되지 않으면(즉, PopupMenu 바깥쪽을 클릭할 시)
 *      Child로 있는 PopupMenu를 inactive한다.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupMenuCanvas : MonoBehaviour {

    private GraphicRaycaster gr;

    private void Start()
    {
        gr = GetComponent<GraphicRaycaster>();

        //SetChildrenActive(false);
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
