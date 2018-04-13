using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class switching_camera : MonoBehaviour {
    int s_w, s_h;
    public Camera caAr;     //AR camera
    public Camera ca;       //main camera
    
    public GameObject masterGO;
    public GameObject dupmasterGO;
    public Component[] GOarr;
    public GameObject[] originGO;

    public Text bttext;
    public Text testtext;
    public Text testtext2;

    // Use this for initialization
    void Start()
    {
        caAr.enabled = true;
        ca.enabled = false;
        GameObject.Find("3dCanvas").GetComponent<Canvas>().enabled = false;

        //Setting default
        //why?? buttonclick event로 구현하기에 초기 설정이 되어있지 않음
        //다른방법으로는 update문에 해당 buttonclick() 함수 내용을 넣고
        //eventListener를 통해 추가시키는 방법 있을것.
        bttext = GameObject.Find("bttext").GetComponent<Text>();
        testtext = GameObject.Find("testtext").GetComponent<Text>();
        testtext2 = GameObject.Find("testtext2").GetComponent<Text>();
        
        s_w = Screen.width;
        s_h = Screen.height;
        //        GameObject.Find("3dCanvas").GetComponent<Canvas>();
    }

    public void ButtonClick()
    {
        //Vuforia.VuforiaBehaviour.Instance.enabled = false;
        //change ar
        if (bttext.text.Equals("caAr"))
        {
            bttext.text = "ca";

            originGO = SceneManager.GetSceneByName("ar").GetRootGameObjects();
            int masterPindex = 0;
            foreach (GameObject tempGO in originGO)
            {
                if (tempGO.transform.name.Equals("3dMasterPlane"))
                {
                    dupmasterGO = originGO[masterPindex];
                }
                else if (tempGO.transform.name.Equals("3dCanvas"))
                {
                    tempGO.GetComponent<Canvas>().enabled = false;
                }
                else if (tempGO.transform.name.Equals("arCanvas"))
                {
                    tempGO.GetComponent<Canvas>().enabled = true;
                }
                masterPindex++;
            }
//            testtext2.text = masterPindex.ToString();

            //destory dupGO
            GOarr = dupmasterGO.GetComponentsInChildren<Component>(true);
//            testtext2.text = GOarr.Length.ToString();
            foreach (Component tempGO in GOarr)
            {
                //name으로 찾을 시 기존 원본까지 삭제 됨으로 다시 바꿀것.
                //밑의 tempdGO.name 수정이 되지 않았음. tempGO.name+"1"->수행x됨
                //destory each of gameobject
                if (tempGO.transform.name.Equals("3dMasterPlane") || tempGO.transform.name.Equals("3D_Camera"))
                {
                    continue;
                }
                Destroy(tempGO.gameObject);
            }
            

            //turn on caAr
            caAr.enabled = true;
            ca.enabled = false;
            //SceneManager.LoadScene("ar");
        }
        else if (bttext.text.Equals("ca"))
        {
            bttext.text = "caAr";

            //turn on ca
            //make clone plane, view
            GOarr = masterGO.GetComponentsInChildren<Component>(true);
            GameObject tempdGO;
            
            ca.enabled = true;
            caAr.enabled = false;

            testtext.text = GOarr.Length.ToString();
            //SceneManager.LoadScene("3d");

            originGO = SceneManager.GetSceneByName("ar").GetRootGameObjects();

            int masterPindex=0;
            //testtext.text = masterPindex.ToString();
            //testtext2.text = originGO.Length.ToString();
            foreach (GameObject tempGO in originGO)
            {
                if (tempGO.transform.name.Equals("3dMasterPlane"))
                {
                    dupmasterGO = originGO[masterPindex];
                }
                else if (tempGO.transform.name.Equals("3dCanvas"))
                {
                    tempGO.GetComponent<Canvas>().enabled = true;
                }
                else if (tempGO.transform.name.Equals("arCanvas"))
                {
                    tempGO.GetComponent<Canvas>().enabled = false;
                }
                masterPindex++;
                //testtext2.text = masterPindex.ToString();
            }
            //dupmasterGO = SceneManager.GetSceneByName("3dMasterPlane").GetRootGameObjects();
            //testtext2.text = masterPindex.ToString();
            //foreach (var tempGO in GOarr)
            //{
            //make new gameobject
                Vector3 s = mergelocation(GOarr[0].transform.localPosition + Vector3.up/10, dupmasterGO.transform.position);
                tempdGO = Instantiate(GameObject.Find(GOarr[0].transform.name), s, GOarr[0].transform.localRotation);
                tempdGO.transform.localScale = new Vector3(100, 100, 100);
                //testtext.text = GOarr[2].name;
                //tempdGO.name = (tempGO.name + "1");
                //tempdGO = Instantiate(tempGO) as GameObject;

                tempdGO.transform.parent = dupmasterGO.transform;
                // tempdGO.transform.localPosition = tempGO.localPosition;
                //testtext2.text = tempdGO.transform.name;
            //}


        }
    }

    Vector3 mergelocation(Vector3 temp, Vector3 master)
    {
        return (temp + master);
    }

    // Update is called once per frame
    void Update()
    {

    }
}