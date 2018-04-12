using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveInDelay : MonoBehaviour {

    private void OnEnable()
    {
        Invoke("SetInactiveThisGameObject", 2.0f);
    }

    private void SetInactiveThisGameObject()
    {
        gameObject.SetActive(false);
    }
}
