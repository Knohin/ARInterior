using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOption : MonoBehaviour {
    
    private BoxCollider[] colliders;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

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

    void Hide() {
        if(gameObject.activeInHierarchy) gameObject.SetActive(false);
    }
}
