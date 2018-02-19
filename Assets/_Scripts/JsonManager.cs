using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager : MonoBehaviour {

    public GameObject ObjectToSave;
    public Text TextField;

    private void Start()
    {
        if (ObjectToSave.GetComponent<SerializedObject>() == null)
        {
            Debug.LogError("Object can't save");
            return;
        }
        SaveObjectTo("path:");
    }

    public void SaveObjectTo(string path)
    {
        string json = JsonUtility.ToJson((Object)ObjectToSave.GetComponent<SerializedObject>());
        TextField.text = json;
    }

    public void LoadObjectFrom(string path)
    {
        // TOOD: Implement
    }
}
