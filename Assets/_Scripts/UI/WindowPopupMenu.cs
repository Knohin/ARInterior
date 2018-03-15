/*
 * 창문용 메뉴
 * 버튼에 에니메이션 작동하는거 달아줌
 * 이거 어떻게 해야될지 모르겟다
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowPopupMenu : PopupMenu {  // PopupMenu를 상속받는다

    //public GameObject ObjectToControl; // Leveled up to Parent Object
    
    private Animator animator;

    private Button OpenLeftButton;
    private Button OpenRightButton;
    private Button CloseButton;

    private void Awake()
    {
        OpenLeftButton = transform.Find("OpenLeftButton").GetComponent<Button>();
        OpenRightButton = transform.Find("OpenRightButton").GetComponent<Button>();
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        if (!(OpenLeftButton && OpenRightButton && CloseButton))
            Debug.LogError("Some Button is missing!");

        OpenLeftButton.onClick.AddListener(OpenWindowA);
        OpenRightButton.onClick.AddListener(OpenWindowB);
        CloseButton.onClick.AddListener(CloseAll);
    }
    
    private void OnEnable()
    {
        animator = ObjectToControl.GetComponent<Animator>();
        if (null == animator)
            Debug.LogError("No Animator!");

        OpenLeftButton.interactable = true;
        OpenRightButton.interactable = true;
        CloseButton.interactable = true;
        int state = animator.GetInteger("WindowState");
        switch (state)  // state에 따라서 버튼 비활성화
        {
            case 0: OpenLeftButton.interactable = false; break;
            case 1: CloseButton.interactable = false;    break;
            case 2: OpenRightButton.interactable = false; break;
        }
    }

    public void OpenWindowA()
    {
        animator.SetInteger("WindowState", 0);
        gameObject.SetActive(false);
    }
    public void OpenWindowB()
    {
        animator.SetInteger("WindowState", 2);
        gameObject.SetActive(false);
    }
    public void CloseAll()
    {
        animator.SetInteger("WindowState", 1);
        gameObject.SetActive(false);
    }
    
}
