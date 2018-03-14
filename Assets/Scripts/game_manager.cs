using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*
        GameObject[] objarr= SceneManager.GetSceneByName("3d").GetRootGameObjects();
        foreach (GameObject tempGO in objarr)
        {
            if (tempGO.transform.name.Equals("Canvas"))
            {
                tempGO.GetComponent<Canvas>().enabled = false;
            }
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
