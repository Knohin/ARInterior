/*
 * 필드 변수 popupMenu에 등록되 있는 메뉴를 설정한다.
 * 변수에 등록되 있는 게 없으면,
 * 자식 오브젝트 중에 PopupMenu 컴포넌트를 찾아서 변수에 저장해둔다.
 * 
 * popupMenu는 ObjectPicker가 메뉴를 카메라 앞에 띄울떄 사용된다.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Pickable : MonoBehaviour {

    public PopupMenu popupMenu;

    private void Awake()
    {
        if (popupMenu == null)
        {
            popupMenu = GetComponentInChildren<PopupMenu>(true);
            if (popupMenu == null)   Debug.LogError("PopupMenu doesn't Exist in Child Objects");
        }
        popupMenu.ObjectToControl = this.gameObject;
    }
}
