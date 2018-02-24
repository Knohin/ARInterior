using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowMenu : PopupMenu {
    
    public Button OpenAButton;
    public Button OpenBButton;
    public Button CloseButton;

    private ObjectPicker picker;
    private Animator animator;

    private void Awake()
    {
        picker = Camera.main.GetComponent<ObjectPicker>();

        OpenAButton.onClick.AddListener(OpenWindowA);
        OpenBButton.onClick.AddListener(OpenWindowB);
        CloseButton.onClick.AddListener(CloseAll);
    }

    private void OnEnable()
    {
        animator = picker.PickedObject.GetComponent<Animator>();
        if (null == animator)
            Debug.LogError("No Animator!");
        
        OpenAButton.interactable = true;
        OpenBButton.interactable = true;
        CloseButton.interactable = true;
        int state = animator.GetInteger("WindowState");
        switch (state)
        {
            case 0: OpenAButton.interactable = false; break;
            case 1: CloseButton.interactable = false; break;
            case 2: OpenBButton.interactable = false; break;
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

    private void OnDisable()
    {
        picker.PickedObject = null;
        animator = null;
    }
}
