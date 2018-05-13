using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToSelectUI() {
        SceneManager.LoadScene("Loading");
    }

    public void ToSave() {
        //SceneManager.LoadScene("");
    }

    public void ToLoad() {

    }

    public void ToSeek() {
        
    }
}