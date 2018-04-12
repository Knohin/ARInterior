using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour {

    private WallBuilder wallBuilder;

    private void Start()
    {
        wallBuilder = GameObject.Find("WallBuilder").GetComponent<WallBuilder>();
    }

    public void OnSaveButtonClicked()
    {
        string fileName = this.GetComponentInChildren<InputField>().text;
        wallBuilder.SaveWall(fileName);

        wallBuilder.Initialize();
        gameObject.SetActive(false);
    }
    public void OnCancleButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
