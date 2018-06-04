using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeRoom : MonoBehaviour {

    public float Width;
    public float Length;

    private float widthOnTable;
    private float lenghtOnTable;

    // Use this for initialization
    void Start () {
        // 크기 실크기에 맞게 비율(배수) 조절
        // 다른 큐브의 세부적인 속성 변경 -> collider, material 등

        const float Thickness = 0.002f;
        const float THalf = Thickness / 2.0f;
        widthOnTable = Width / 30.0f;
        lenghtOnTable = Length / 30.0f;

        // West
        GameObject edge1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge1.transform.localScale = new Vector3(Thickness, Thickness, widthOnTable);
        edge1.transform.position = new Vector3(-THalf, THalf, -widthOnTable / 2.0f);
        edge1.GetComponent<MeshRenderer>().material.color = Color.black;
        edge1.GetComponent<BoxCollider>().center = new Vector3(-0.001f, 0.0f, 0.0f);
        // East
        GameObject edge2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge2.transform.localScale = new Vector3(Thickness, Thickness, widthOnTable);
        edge2.transform.position = new Vector3(lenghtOnTable + THalf, THalf, -widthOnTable / 2.0f);
        edge2.GetComponent<MeshRenderer>().material.color = Color.black;
        edge1.GetComponent<BoxCollider>().center = new Vector3(0.001f, 0.0f, 0.0f);
        // North
        GameObject edge3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge3.transform.localScale = new Vector3(lenghtOnTable, Thickness, Thickness);
        edge3.transform.position = new Vector3(lenghtOnTable / 2.0f, THalf, -THalf);
        edge3.GetComponent<MeshRenderer>().material.color = Color.black;
        edge1.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, 0.001f);
        // South
        GameObject edge4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        edge4.transform.localScale = new Vector3(lenghtOnTable, Thickness, Thickness);
        edge4.transform.position = new Vector3(lenghtOnTable / 2.0f, THalf, -widthOnTable + THalf);
        edge4.GetComponent<MeshRenderer>().material.color = Color.black;
        edge1.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, -0.001f);

        edge1.transform.SetParent(this.transform);
        edge2.transform.SetParent(this.transform);
        edge3.transform.SetParent(this.transform);
        edge4.transform.SetParent(this.transform);
    }
	
}


//////////////////////////////////////////
//void Start()
//{
//    // 크기 실크기에 맞게 비율(배수) 조절
//    // 다른 큐브의 세부적인 속성 변경 -> collider, material 등

//    width *= 30.0f;
//    length *= 30.0f;

//    GameObject edge1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
//    edge1.transform.localScale = new Vector3(3.0f, 3.0f, width);
//    edge1.transform.position = new Vector3(-1.5f, 1.5f, -width / 2.0f);
//    edge1.GetComponent<MeshRenderer>().material.color = Color.black;
//    edge1.GetComponent<MeshRenderer>().material.SetFloat("_Metalic/_Smoothness", 0.0f);
//    edge1.GetComponent<BoxCollider>().center = new Vector3(-1.0f, 0.0f, 0.0f);

//    GameObject edge2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
//    edge2.transform.localScale = new Vector3(3.0f, 3.0f, width);
//    edge2.transform.position = new Vector3(length + 1.5f, 1.5f, -width / 2.0f);
//    edge2.GetComponent<MeshRenderer>().material.color = Color.black;
//    edge2.GetComponent<MeshRenderer>().material.SetFloat("_Metalic/_Smoothness", 0.0f);
//    edge2.GetComponent<BoxCollider>().center = new Vector3(1.0f, 0.0f, 0.0f);

//    GameObject edge3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
//    edge3.transform.localScale = new Vector3(length + 6.0f, 3.0f, 3.0f);
//    edge3.transform.position = new Vector3(length / 2.0f, 1.5f, 1.5f);
//    edge3.GetComponent<MeshRenderer>().material.color = Color.black;
//    edge3.GetComponent<MeshRenderer>().material.SetFloat("_Metalic/_Smoothness", 0.0f);
//    edge3.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, 1.0f);

//    GameObject edge4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
//    edge4.transform.localScale = new Vector3(length + 6.0f, 3.0f, 3.0f);
//    edge4.transform.position = new Vector3(length / 2.0f, 1.5f, -width - 1.5f);
//    edge4.GetComponent<MeshRenderer>().material.color = Color.black;
//    edge4.GetComponent<MeshRenderer>().material.SetFloat("_Metalic/_Smoothness", 0.0f);
//    edge4.GetComponent<BoxCollider>().center = new Vector3(0.0f, 0.0f, -1.0f);
//}