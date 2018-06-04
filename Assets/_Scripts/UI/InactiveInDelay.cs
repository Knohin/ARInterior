using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveInDelay : MonoBehaviour {

    public ModeManager modeManager;

    private void OnEnable()
    {
        Invoke("SetInactiveThisGameObject", 2.0f);
    }
    private void OnDisable()
    {
        modeManager.SetMode(0);
    }

    private void SetInactiveThisGameObject()
    {
        gameObject.SetActive(false);
    }
}
