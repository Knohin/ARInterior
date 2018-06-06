using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour {

    public GameObject[] CanvasOfModes;

    public void SetMode(int i)
    {
        foreach(var canvas in CanvasOfModes)
            canvas.SetActive(false);
        CanvasOfModes[i].SetActive(true);
    }

}
