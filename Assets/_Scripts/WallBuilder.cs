using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WallBuilder : MonoBehaviour {

    // 나중에 private으로
    public int touchCount;
    public Vector3[] points;

    private void Start()
    {
        touchCount = 0;
        points = new Vector3[4];
    }

    private void Update()
    {
        Vector2 pos;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
#else
        if (0 < Input.touchCount
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pos = Input.GetTouch(0).position;
#endif
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                MakeSphere(hit.point);

                points[touchCount] = hit.point;

                touchCount++;
                if (touchCount == 4)
                {
                    BuildWall();
                    touchCount = 0;
                }
            }
        }
    }

    private void MakeSphere(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        Destroy(sphere.GetComponent<Collider>());
    }

    private void BuildWall()
    {
        GameObject obj = new GameObject("Wall");
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        Mesh newMesh = new Mesh();
        mf.mesh = newMesh;

        newMesh.vertices = new Vector3[] {
            points[0], points[0]+Vector3.up,
            points[1], points[1]+Vector3.up,
            points[2], points[2]+Vector3.up,
            points[3], points[3]+Vector3.up
        };

        newMesh.triangles = new int[] {
            // <CW> , <CCW>   // 양면 다 그림
            0,1,3,  3,1,0,
            0,3,2,  2,3,0,
            2,3,5,  5,3,2,
            2,5,4,  4,5,2,
            4,5,7,  7,5,4,
            4,7,6,  6,7,4,
            6,7,1,  1,7,6,
            6,1,0  ,0,1,6
        };
        newMesh.RecalculateBounds();
        //newMesh.RecalculateNormals(); // 이걸 쓰려면 코너부분 버텍스를 추가해 줘야 한다
        //newMesh.RecalculateTangents();


        mr.material = new Material(Shader.Find("Standard"));
        //mr.material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Material.mat");
    }
}
