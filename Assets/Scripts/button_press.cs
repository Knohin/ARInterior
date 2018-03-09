using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//IPointerDownHandler, IPointerUpHandler -> for checking button pressed
public class button_press : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    enum buttonType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        BLANK
    };
    buttonType button_type;
    bool button_state;
    float speed;
    public Camera mainCa;
    Image curimage;
    Sprite popSpr, pushSpr;
    void Start()
    {
        button_state = false;
        speed = 2.0f;
        
        curimage = this.gameObject.GetComponent<Image>();
    }
	// Update is called once per frame
	void Update () {
        //check button pressed
         if (button_state)
         {
            if (button_type == buttonType.UP)
            {
                Moveup();
            }
            else if (button_type == buttonType.DOWN)
            {
                Movedown();
            }
            else if (button_type == buttonType.LEFT)
            {
                Moveleft();
            }
            else if (button_type == buttonType.RIGHT)
            {
                Moveright();
            }
        }	
	}

    //Camera. transform.Translate(Vector3.forward * Speed *Time.deltaTime);
    //forward moving
    void Moveup()
    {
        mainCa.transform.Translate(Vector3.forward * speed);
    }

    //backward moving
    void Movedown()
    {
        mainCa.transform.Translate(Vector3.back * speed);
    }
    
    void Moveleft()
    {
        mainCa.transform.Translate(Vector3.left * speed);
    }

    void Moveright()
    {
        mainCa.transform.Translate(Vector3.right * speed);
    }

    //Button press state interface implement
    public void OnPointerDown(PointerEventData ped)
    {
        if (this.gameObject.transform.name.Equals("Up_Button"))
        {
            button_type = buttonType.UP;
            popSpr = Resources.Load("image/pop_up") as Sprite;
            pushSpr = Resources.Load("image/push_up") as Sprite;
        }
        else if (this.gameObject.transform.name.Equals("Down_Button"))
        {
            button_type = buttonType.DOWN;
            popSpr = Resources.Load("image/pop_down") as Sprite;
            pushSpr = Resources.Load("image/push_down") as Sprite;
        }
        else if (this.gameObject.transform.name.Equals("Left_Button"))
        {
            button_type = buttonType.LEFT;
            popSpr = Resources.Load("image/pop_left") as Sprite;
            pushSpr = Resources.Load("image/push_left") as Sprite;
        }
        else if (this.gameObject.transform.name.Equals("Right_Button"))
        {
            button_type = buttonType.RIGHT;
            popSpr = Resources.Load("image/pop_right") as Sprite;
            pushSpr = Resources.Load("image/push_right") as Sprite;
        }
        curimage.overrideSprite = pushSpr;
        button_state = true;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        curimage.overrideSprite = popSpr;
        button_state = false;
    }
}
