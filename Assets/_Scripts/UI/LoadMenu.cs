using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class LoadMenu : MonoBehaviour {

    public Transform BaseTransform;

    // 파일 리스트의 UI 그룹
    private GameObject mContent;
    //private string[] mFilePaths;
    private int mSelectedFileIdx;

    private GraphicRaycaster gr;
    private JsonManager jsonManager;

    private void Awake()
    {
        mContent = transform.GetComponentInChildren<VerticalLayoutGroup>().gameObject; //.Find("Scroll View").Find("Viewport").Find("Content").gameObject;

        jsonManager = GameObject.Find("JsonManager").GetComponent<JsonManager>();
        gr = GetComponentInParent<GraphicRaycaster>();

        if (mContent == null || gr == null || jsonManager == null)
            Debug.LogError("Private member variable is null !!!fsiojf83");
    }

    private void OnEnable()
    {
        // Read File name
#if UNITY_EDITOR
        string[] mFilePaths = Directory.GetFiles(Application.dataPath + "/Resources");
#else
        string[] mFilePaths = Directory.GetFiles(Application.persistentDataPath + "/Resources");
#endif
        string[] fileNames = new string[mFilePaths.Length];

        int idx=0;
        foreach (var file in mFilePaths)
        {
            int filenameIndex = (file.LastIndexOf('\\') > file.LastIndexOf('/')) ? file.LastIndexOf('\\')+1 : file.LastIndexOf('/')+1;
            string filename = file.Substring(filenameIndex);
            //if (filename.Contains(".json"))
            //    filename = filename.Remove(filename.IndexOf('.'));
            fileNames[idx++] = filename;
        }
        
        // Set Children Content by read name
        foreach (var filename in fileNames)
        {
            GameObject go = Instantiate(mContent.transform.GetChild(0).gameObject);
            go.transform.SetParent(mContent.transform);
            go.GetComponentInChildren<Text>().text = filename;
            go.SetActive(true);
        }
    }
    private void OnDisable()
    {
        // Clear Children of Content Except first one
        for (int i = 1; i < mContent.transform.childCount; ++i)
            Destroy(mContent.transform.GetChild(i).gameObject);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;
#else
        if (0 < Input.touchCount
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 pos = Input.GetTouch(0).position;
#endif
            //Create the PointerEventData with null for the EventSystem
            PointerEventData ped = new PointerEventData(null);
            ped.position = pos;

            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);

            if (results.Count > 0 && results[0].gameObject.transform.parent.gameObject == mContent)
            {
                mSelectedFileIdx = results[0].gameObject.transform.GetSiblingIndex();
                for (int i = 0; i < mContent.transform.childCount; ++i)
                    mContent.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                mContent.transform.GetChild(mSelectedFileIdx).GetComponent<Image>().color = new Color(244.0f / 255, 214.0f / 255, 147.0f / 255, 1);
            }
        }
    }

    public void OnLoadButtonClicked()
    {
        string fileName = mContent.transform.GetChild(mSelectedFileIdx).GetComponentInChildren<Text>().text;
        //GameObject wall = WallBuilder.LoadWall(fileName);
        GameObject interiorGroup = jsonManager.LoadObjectFromJson(fileName);
        interiorGroup.transform.position = BaseTransform.position;
        interiorGroup.transform.rotation = BaseTransform.rotation;
        interiorGroup.transform.localScale = Vector3.one * 15*2;
        
        GameObject.Find("Plane Finder").GetComponent<DeployStageOnce>().PerformHitTestToScreenCenter();


        gameObject.SetActive(false);
    }

    public void OnCancleButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
