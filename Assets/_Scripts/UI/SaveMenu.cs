using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveMenu : MonoBehaviour {

    private JsonManager jsonManager;

    public GameObject[] ObjectToSaveInEditor;

    private void Start()
    {
        jsonManager = GameObject.Find("JsonManager").GetComponent<JsonManager>();
        if (jsonManager == null)
            Debug.LogError("Component Miss!!! 12099ufs0");
    }

    public void OnSaveButtonClicked()
    {
        string fileName = this.GetComponentInChildren<InputField>().text;
        jsonManager.SaveObjectInJson(fileName);
        
        gameObject.SetActive(false);
    }
    public void OnCancleButtonClicked()
    {
        gameObject.SetActive(false);
    }

    public void SaveInEditor(string filename = "SaveTable01.json")
    {
        Vuforia.StateManager stateManager = Vuforia.TrackerManager.Instance.GetStateManager();

        List<SerializedData> list = new List<SerializedData>();

        // 인식된 가구들
        var trackable = stateManager.GetActiveTrackableBehaviours();
        if (trackable == null) Debug.Log("Trackable : " + trackable);

        // Warning : 아니 이게 유니티 애디터에서는 아래 포문을 돌지 않는데 
        // 안드로이드로 빌드하면 한번을 도는 문제가 있다.
        foreach (var t in ObjectToSaveInEditor)
        {
            Debug.Log("trackables : " + t);
            SerializedObject so = t.GetComponentInChildren<SerializedObject>();
            if (so == null)
            {
                Debug.LogError("Object can't save");
                continue;
            }
            // list에 넣기 전 현상태 불러오기
            GameObject ObjectToSave = t.gameObject.GetComponentInChildren<SerializedObject>().gameObject;
            ObjectToSave.GetComponent<SerializedObject>().SetCurrentState();
            list.Add(ObjectToSave.GetComponent<SerializedObject>().sd);
        }
        // 배열의 내용을 json형식 string으로 저장
        string json = JsonHelper.ToJson<SerializedData>(list.ToArray());

        // 저장 디렉토리 존재여부
        if (!Directory.Exists(Application.dataPath + "/Resources"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");
        }
        string path = Path.Combine(Application.dataPath + "/Resources", filename);

        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(json);
        sw.Close();
        file.Close();
    }
}
