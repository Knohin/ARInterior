using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeRoom : MonoBehaviour {

    public GameObject house1;
    public GameObject house2;
    public GameObject edge1, edge2, edge3, edge4;

    float width, length;

	// Use this for initialization
	void Start () {
        // 크기 실크기에 맞게 비율(배수) 조절
        // 다른 큐브의 세부적인 속성 변경 -> collider, material 등

        house1.SetActive(false);
        house2.SetActive(false);
        edge1.SetActive(false);
        edge2.SetActive(false);
        edge3.SetActive(false);
        edge4.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        width = GlobalVariable.width;
        length = GlobalVariable.length;

        makeSpace();
	}

    void makeSpace() {
        switch(GlobalVariable.select)
        {
            case 0:
                house2.SetActive(false);
                edge1.SetActive(false);
                edge2.SetActive(false);
                edge3.SetActive(false);
                edge4.SetActive(false);

                house1.SetActive(true);
                break;
            case 1:
                house1.SetActive(false);
                edge1.SetActive(false);
                edge2.SetActive(false);
                edge3.SetActive(false);
                edge4.SetActive(false);

                house2.SetActive(true);
                break;
            case 2:
                house1.SetActive(false);
                house2.SetActive(false);

                edge1.transform.localScale = new Vector3(0.3f, 0.3f, width) / 30.0f;
                edge1.transform.localPosition= new Vector3(-0.15f, 0.15f, -width / 2.0f) / 30.0f;
                edge1.GetComponent<MeshRenderer>().material.color = Color.black;
                edge1.GetComponent<BoxCollider>().center = new Vector3(-0.5f, 0.0f, 0.0f);

                edge2.transform.localScale = new Vector3(0.3f, 0.3f, width) / 30.0f;
                edge2.transform.localPosition = new Vector3(length + 0.15f, 0.15f, -width / 2.0f) / 30.0f;
                edge2.GetComponent<MeshRenderer>().material.color = Color.black;
                edge2.GetComponent<BoxCollider>().center = new Vector3(0.5f, 0.0f, 0.0f);

                edge3.transform.localScale = new Vector3(length + 0.6f, 0.3f, 0.3f) / 30.0f;
                edge3.transform.localPosition = new Vector3(length / 2.0f, 0.15f, 0.15f) / 30.0f;
                edge3.GetComponent<MeshRenderer>().material.color = Color.black;
                edge3.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, 0.5f);

                edge4.transform.localScale = new Vector3(length + 0.6f, 0.3f, 0.3f) / 30.0f;
                edge4.transform.localPosition = new Vector3(length / 2.0f, 0.15f, -width - 0.15f) / 30.0f;
                edge4.GetComponent<MeshRenderer>().material.color = Color.black;
                edge4.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, -0.5f);

                edge1.SetActive(true);
                edge2.SetActive(true);
                edge3.SetActive(true);
                edge4.SetActive(true);

                break;
        }
    }
}
