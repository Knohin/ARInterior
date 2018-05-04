using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour {

    private JsonManager jsonManager;
    private WallBuilder wallBuilder;

    private void Start()
    {
        jsonManager = GameObject.Find("JsonManager").GetComponent<JsonManager>();
        wallBuilder = GameObject.Find("WallBuilder").GetComponent<WallBuilder>();
        if (jsonManager == null || wallBuilder == null)
            Debug.LogError("12099ufs0");
    }

    public void OnSaveButtonClicked()
    {
        string fileName = this.GetComponentInChildren<InputField>().text;
        //wallBuilder.SaveWall(fileName);
        jsonManager.SaveObjectInJson(fileName);

        wallBuilder.Initialize();
        gameObject.SetActive(false);
    }
    public void OnCancleButtonClicked()
    {
        wallBuilder.Initialize();
        gameObject.SetActive(false);
    }
}
