using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour {

    public Camera MainCamera;

    public Pickable PickedObject;

    private bool touchDowned = false;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            Physics.Raycast(ray, out hit);

            PickedObject = hit.collider.gameObject.GetComponent<Pickable>();

        }
        if (0 < Input.touchCount
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            PickedObject = hit.collider.gameObject.GetComponent<Pickable>();
        }
    }
}
