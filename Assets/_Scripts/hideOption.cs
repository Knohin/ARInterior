using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOption : MonoBehaviour {

    //private GameObject arCamera;
    //Vector3 dirVector;
    private BoxCollider[] colliders;

    // Use this for initialization
    void Start() {
        //arCamera = GameObject.Find("ARCamera");
        //dirVector = arCamera.transform.position - transform.position;
    }

    void OnEnable() {
        colliders = gameObject.GetComponentsInParent<BoxCollider>();
        foreach (BoxCollider collider in colliders)
            collider.enabled = false;

        Invoke("Hide", 3);
    }

    void OnDisable() {
        foreach (BoxCollider collider in colliders)
            collider.enabled = true;

        CancelInvoke();
    }

    // Update is called once per frame
    void Update() {
        // 나 뭐할라 했었지...?
        // OnEnable,OnDisalbe과 Invoke로 해결!
        //Debug.Log(arCamera.transform.position);
        //gameObject.transform.position = gameObject.GetComponentInParent<GameObject>().transform.position
        //    + Vector3.up * 3;
        //gameObject.transform.LookAt(arCamera.transform.position);
    }

    void Hide() {
        if(gameObject.activeInHierarchy) gameObject.SetActive(false);
    }
}
