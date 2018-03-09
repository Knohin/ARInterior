using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour {

    public Camera caAr;     //AR camera
    public Camera caAr2;     //3d's AR camera
    public Camera ca;       //main camera

    public void SceneChangear()
    {
        caAr.enabled = true;
        caAr2.enabled = false;
        ca.enabled = false;
        SceneManager.LoadScene("ar");
    }

    public void SceneChange3d()
    {
        ca.enabled = true;
        caAr.enabled = false;
        caAr2.enabled = false;
        SceneManager.LoadScene("3d");
    }
}
