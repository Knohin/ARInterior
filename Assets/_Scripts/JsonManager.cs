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

            //Debug.Log(ObjectToLoad.mFurniture);

            string name = ObjectToLoad.mFurniture.Trim();
            name = name.Substring(0, name.IndexOf("(") - 1);

            //Debug.Log(name);

            switch (name)
            {
                case "Cube":
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    setFurniture(cube, ObjectToLoad);
                    break;
                case "Sphere":
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    setFurniture(sphere, ObjectToLoad);
                    break;
                case "Capsule":
                    GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    setFurniture(capsule, ObjectToLoad);
                    break;
                case "bed":
                    GameObject.Instantiate(bed);
                    setFurniture(bed, ObjectToLoad);
                    break;
                case "bookshelf":
                    GameObject.Instantiate(bookshelf);
                    setFurniture(bookshelf, ObjectToLoad);
                    break;
                case "desk":
                    GameObject.Instantiate(desk);
                    setFurniture(desk, ObjectToLoad);
                    break;
                case "gasrange":
                    GameObject.Instantiate(gasrange);
                    setFurniture(gasrange, ObjectToLoad);
                    break;
                case "oven":
                    GameObject.Instantiate(oven);
                    setFurniture(oven, ObjectToLoad);
                    break;
                case "refrigertor":
                    GameObject.Instantiate(refrigertor);
                    setFurniture(refrigertor, ObjectToLoad);
                    break;
                case "sink":
                    GameObject.Instantiate(sink);
                    setFurniture(sink, ObjectToLoad);
                    break;
                case "sofa":
                    GameObject.Instantiate(sofa);
                    setFurniture(sofa, ObjectToLoad);
                    break;
                case "table":
                    GameObject.Instantiate(table);
                    setFurniture(table, ObjectToLoad);
                    break;
                case "television":
                    GameObject.Instantiate(television);
                    setFurniture(television, ObjectToLoad);
                    break;
                case "wardrobe":
                    GameObject.Instantiate(wardrobe);
                    setFurniture(wardrobe, ObjectToLoad);
                    break;
            }
        }

        else
        {
        }
    }

    public void setFurniture(GameObject furniture, SerializedData ObjectToLoad)
    {
        furniture.transform.position = ObjectToLoad.mPosition;
        furniture.transform.rotation = ObjectToLoad.mRotation;
        furniture.transform.localScale = ObjectToLoad.mScale; 
        //furniture.GetComponent<MeshRenderer>().material.color = ObjectToLoad.mColor;
    }
}
