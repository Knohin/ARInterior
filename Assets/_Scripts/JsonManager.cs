using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager : MonoBehaviour
{
    public GameObject ObjectToSave;
    private Text TextFieldToPrint, TextFieldToPrint2;
    public GameObject bed, bookshelf, desk, gasrange, oven, refrigertor, sink, sofa, table, television, wardrobe;


    private void Start()
    {
        // 출력할 Text 찾기
        TextFieldToPrint = GameObject.Find("TextForDebug").GetComponent<Text>();
        TextFieldToPrint2 = GameObject.Find("LoadText").GetComponent<Text>();

        // 버튼에 이벤트 달기
        Button PrintButton = GameObject.Find("Button").GetComponent<Button>();
        PrintButton.onClick.AddListener(SaveObjectInJson);

        Button PrintButton2 = GameObject.Find("Button (2)").GetComponent<Button>();
        PrintButton2.onClick.AddListener(LoadObjectFromJson);
    }

    public void SaveObjectInJson()
    {
        if (ObjectToSave.GetComponent<SerializedObject>().sd == null)
        {
            Debug.LogError("Object can't save");
            return;
        }

        string json = JsonUtility.ToJson((object)ObjectToSave.GetComponent<SerializedObject>().sd);
        TextFieldToPrint.text = json; // TextFieldToPrint의 텍스트를 바꾼다. 기능 구현하면 주석처리

        if (!Directory.Exists(Application.persistentDataPath + "/Resources"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Resources");
        }
        //File.WriteAllText(Application.dataPath + "/Resources/data.json", json); // 컴퓨터
        string path = pathForDocumentsFile("data.json");
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(json);
        sw.Close();
        file.Close();
    }

    public string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5) + "/Resources";
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath + "/Resources";
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }

        else
        {
            string path = Application.dataPath + "/Resources";
            return Path.Combine(path, filename);
        }
    }

    public void LoadObjectFromJson()
    {        
        string filename = "data.json";
        string path = pathForDocumentsFile(filename);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str = null;
            str = sr.ReadLine();

            sr.Close();
            file.Close();
            
            TextFieldToPrint2.text = str;

            SerializedData ObjectToLoad = JsonUtility.FromJson<SerializedData>(str);
            
            Debug.Log(ObjectToLoad.ToString());

            string name = ObjectToLoad.mFurniture.Trim();

            if (name.IndexOf("(") == 1)
            {
                name = name.Substring(0, name.IndexOf("(") - 1);
            }

            //Debug.Log(name);
            GameObject madeObject = new GameObject();
            switch (name)
            {
                case "Wall":
                    madeObject = new GameObject("Wall");
                    MeshFilter mf = madeObject.AddComponent<MeshFilter>();
                    MeshRenderer mr = madeObject.AddComponent<MeshRenderer>();
                    madeObject.AddComponent<SerializedObject> ();
                    
                    mr.material = new Material(Shader.Find("Standard"));
                    break;

                case "bed":
                    madeObject = GameObject.Instantiate(bed);
                    break;
                case "bookshelf":
                    madeObject = GameObject.Instantiate(bookshelf);
                    break;
                case "desk":
                    madeObject = GameObject.Instantiate(desk);
                    break;
                case "gasrange":
                    madeObject = GameObject.Instantiate(gasrange);
                    break;
                case "oven":
                    madeObject = GameObject.Instantiate(oven);
                    break;
                case "refrigertor":
                    madeObject = GameObject.Instantiate(refrigertor);
                    break;
                case "sink":
                    madeObject = GameObject.Instantiate(sink);
                    break;
                case "sofa":
                    madeObject = GameObject.Instantiate(sofa);
                    break;
                case "table":
                    madeObject = GameObject.Instantiate(table);
                    break;
                case "television":
                    madeObject = GameObject.Instantiate(television);
                    break;
                case "wardrobe":
                    madeObject = GameObject.Instantiate(wardrobe);
                    break;
            }
            madeObject.GetComponent<SerializedObject>().sd = ObjectToLoad;
            madeObject.GetComponent<SerializedObject>().SetSdToObject();
        }
        else
        {
        }
    }
}
