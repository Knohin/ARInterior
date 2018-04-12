using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class LoadMenu : MonoBehaviour {

    // 파일 리스트의 UI 그룹
    private GameObject mContent;
    //private string[] mFilePaths;
    private int mSelectedFileIdx;

    private GraphicRaycaster gr;

    private void Awake()
    {
        mContent = transform.GetComponentInChildren<VerticalLayoutGroup>().gameObject; //.Find("Scroll View").Find("Viewport").Find("Content").gameObject;

        gr = GetComponentInParent<GraphicRaycaster>();

        if (mContent == null || gr == null)
            Debug.LogError("Private member variable is null !!!");
    }

    private void OnEnable()
    {
        // Read File name
        string[] mFilePaths = Directory.GetFiles(Application.persistentDataPath + "\\Resource");
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
        idx = 0;
        foreach(var filename in fileNames)
        {
            Text text;
            if (idx++ == 0)
            {
                text = mContent.transform.GetChild(0).GetComponentInChildren<Text>();
            }
            else
            {
                GameObject go = Instantiate(mContent.transform.GetChild(0).gameObject);
                go.transform.SetParent(mContent.transform);
                text = go.GetComponentInChildren<Text>();
            }
            text.text = filename;
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
        GameObject wall = WallBuilder.LoadWall(fileName);

        gameObject.SetActive(false);
    }

    public void OnCancleButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
