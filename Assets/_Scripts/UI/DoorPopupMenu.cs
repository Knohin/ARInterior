/*
 * 방문용 메뉴
 * 뭐 그냥 버튼 이벤트 달고 
 * 애니메이션 실행시키고
 * ...
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorPopupMenu : PopupMenu
{
    //public GameObject ObjectToControl; // Leveled up to Parent Object

    private Animator animator;

    private Button OpenButton;
    private Button CloseButton;
    
    private void Awake()
    {
        // TODO: 버튼 세팅
        OpenButton = transform.Find("OpenButton").GetComponent<Button>();
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        if (!(OpenButton && CloseButton))
            Debug.LogError("Some Button is missing!");

        OpenButton.onClick.AddListener(OpenDoor);
        OpenButton.onClick.AddListener(CloseDoor);
    }

    private void OnEnable()
    {
        animator = ObjectToControl.GetComponent<Animator>();
        if (null == animator)
            Debug.LogError("No Animator!");

        if (animator.GetBool("IsOpened"))
        {
            OpenButton.interactable = false;
            CloseButton.interactable = true;
        }
        else
        {
            OpenButton.interactable = true;
            CloseButton.interactable = false;
        }
    }

    public void OpenDoor()
    {
        animator.SetBool("IsOpened", true);
        gameObject.SetActive(false);
    }
    public void CloseDoor()
    {
        animator.SetBool("IsOpened", false);
        gameObject.SetActive(false);
    }
}
