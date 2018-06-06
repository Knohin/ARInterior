using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchObject : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0) {

            Vector2 pos = Input.GetTouch(0).position;
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);

            Ray ray = Camera.main.ScreenPointToRay(theTouch);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    // 터치한 순간
                    GameObject target = hit.collider.gameObject;
                    Transform option = target.transform.Find("Option");
                    BoxCollider[] colliders = target.GetComponents<BoxCollider>();

                    float width = colliders[0].size.x / 2.0f + 1.0f;
                    float height = colliders[0].center.y + 1.0f;

                    if (hit.collider == colliders[0])
                    {
                        //Up
                        option.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
                        option.localPosition = new Vector3(0.0f, height, 0.0f);
                    }
                    else
                    {
                        if (height > 30.0f) height -= 10.0f;
                        else if (height > 15.0f) height -= 5.0f;

                        if (hit.collider == colliders[1])
                        {
                            //Forward
                            option.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                            option.localPosition = new Vector3(0.0f, height, -10.0f);
                        }
                        else if (hit.collider == colliders[2])
                        {
                            //Left
                            option.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                            option.localPosition = new Vector3(width * -1, height, 0.0f);
                        }
                        else if (hit.collider == colliders[3])
                        {
                            //Right
                            option.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
                            option.localPosition = new Vector3(width, height, 0.0f);
                        }
                        else if (hit.collider == colliders[4])
                        {
                            //Backward
                            option.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                            option.localPosition = new Vector3(0.0f, height, 10.0f);
                        }
                    }
                    option.gameObject.SetActive(true);
                }
            }
        }
    }
}
