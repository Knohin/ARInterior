/*
 * 1. 원점을 중심으로 글로벌 좌표계 축을 그린다
 * 2. 이 스크림트가 달린 오브젝트를 중심으로 글로벌 좌표계 축을 그린다
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAxisDrawer : MonoBehaviour {

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void Update ()
    {
        Vector3 pos = this.transform.position;
        Debug.DrawLine(pos, pos + Vector3.right, Color.red);
        Debug.DrawLine(pos, pos + Vector3.up, Color.green);
        Debug.DrawLine(pos, pos + Vector3.forward, Color.blue);
        Debug.DrawLine(Vector3.zero, Vector3.right, Color.red);
        Debug.DrawLine(Vector3.zero, Vector3.up, Color.green);
        Debug.DrawLine(Vector3.zero, Vector3.forward, Color.blue);
    }
}
