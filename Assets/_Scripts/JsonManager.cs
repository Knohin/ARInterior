using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager : MonoBehaviour {

    public GameObject ObjectToSave;
    private Text TextFieldToPrint;

    private void Start()
    {
        if (ObjectToSave.GetComponent<SerializedObject>() == null)
        {
            Debug.LogError("Object can't save");
            return;
        }
        SaveObjectTo("path:");

        // 출력할 Text 찾기
        TextFieldToPrint = GameObject.Find("TextForDebug").GetComponent<Text>();

        // 버튼에 이벤트 달기
        Button PrintButton = GameObject.Find("Button").GetComponent<Button>();
        PrintButton.onClick.AddListener(PrintObjectInJson);
    }

    public void PrintObjectInJson()
    {
        string json = JsonUtility.ToJson((Object)ObjectToSave.GetComponent<SerializedObject>());
        TextFieldToPrint.text = json;
    }

    public void SaveObjectTo(string path)
    {
        // TOOD: Implement
    }

    public void LoadObjectFrom(string path)
    {
    }
}
