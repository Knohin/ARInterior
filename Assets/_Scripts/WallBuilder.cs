/* 
 * 설명 :
 *  클릭(혹은 터치) 를 이용해 4개의 점을 찍으면 그 점을 꼭지점으로 하는 사각형의 벽이 만들어진다.
 *  TODO >> 아직 시계방향 순서로 클릭해야 유효하다.
 * 
 * 사용법 : 
 *  아무 GameObject에 붙이면 된다.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
                    ReviseIndex();
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
        sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //sphere.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f); // 안먹네. 머티리얼을 새로 만들어야할듯
        Destroy(sphere.GetComponent<Collider>());
    }

    private void BuildWall()
    {
        const float Height = 1.0f;
        Vector3 HeightVector = Vector3.up * Height;
        const float Thickness = 0.1f;
        GameObject obj = new GameObject("Wall");
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        Mesh newMesh = new Mesh();
        mf.mesh = newMesh;

        // 내부 벽
        List<Vector3> vertices = new List<Vector3>();
        vertices.AddRange( new Vector3[] {
            points[0], points[0]+HeightVector,
            points[1], points[1]+HeightVector,
            points[1], points[1]+HeightVector,
            points[2], points[2]+HeightVector,
            points[2], points[2]+HeightVector,
            points[3], points[3]+HeightVector,
            points[3], points[3]+HeightVector,
            points[0], points[0]+HeightVector
        });

        // Calculate normals
        List<Vector3> normals = new List<Vector3>();
        for(int i=0; i<16; i+=4)
        {
            Vector3 normal = Vector3.Cross(
                vertices[i + 1] - vertices[i],
                vertices[i + 2] - vertices[i]
                ).normalized;
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
            normals.Add(normal);
        }
        for (int i = 0; i < 16; i += 4)
        {
            Vector3 invNormal = -normals[i];
            normals.Add(invNormal);
            normals.Add(invNormal);
            normals.Add(invNormal);
            normals.Add(invNormal);
        }
        for (int i = 0; i < 8; i++)
            normals.Add(Vector3.up);

        // 외부 벽
        Vector3[] outWallPoints = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            int prev_i = (i == 0) ? 3 : i - 1;
            int next_i = (i == 3) ? 0 : i + 1;
            Vector3 a = points[prev_i] - points[i];
            Vector3 b = points[next_i] - points[i];
            float theta = Mathf.Acos(Vector3.Dot(a, b) / (a.magnitude * b.magnitude));
            theta = (Mathf.PI - theta) / 2.0f;
            float cornerThickness = Thickness / Mathf.Cos(theta);
            Vector3 distance = Quaternion.Euler(Vector3.down * 180*theta/Mathf.PI) * -normals[i * 4];
            distance *= cornerThickness;
            outWallPoints[i] = points[i] + distance;
        }

        vertices.AddRange(new Vector3[] {
            outWallPoints[0], outWallPoints[0]+HeightVector,
            outWallPoints[1], outWallPoints[1]+HeightVector,
            outWallPoints[1], outWallPoints[1]+HeightVector,
            outWallPoints[2], outWallPoints[2]+HeightVector,
            outWallPoints[2], outWallPoints[2]+HeightVector,
            outWallPoints[3], outWallPoints[3]+HeightVector,
            outWallPoints[3], outWallPoints[3]+HeightVector,
            outWallPoints[0], outWallPoints[0]+HeightVector
        });

        // 벽 윗면
        vertices.AddRange(new Vector3[] {
            points[0]+HeightVector,
            points[1]+HeightVector,
            points[2]+HeightVector,
            points[3]+HeightVector,
            outWallPoints[0]+HeightVector,
            outWallPoints[1]+HeightVector,
            outWallPoints[2]+HeightVector,
            outWallPoints[3]+HeightVector
        });

        newMesh.SetVertices(vertices);
        newMesh.SetNormals(normals);

        newMesh.triangles = new int[] {
            // 내부 벽
            //01 23
            0,1,3,     
            0,3,2,     
            //45 67
            4,5,7,     
            4,7,6,     
            //8 9  10 11
            8,9,11,    
            8,11,10,   
            //12 13 14 15
            12,13,15,
            12,15,14
            // 바깥벽
            ,18,19,17,
            18,17,16,
            22,23,21,
            22,21,20,
            26,27,25,
            26,25,24,
            30,31,29,
            30,29,28
            // 벽 윗면
            ,32,36,37,
            32,37,33,
            33,37,38,
            33,38,34,
            34,38,39,
            34,39,35,
            35,39,36,
            35,36,32
        };

        newMesh.RecalculateBounds();
        //newMesh.RecalculateNormals();
        //newMesh.RecalculateTangents();


        mr.material = new Material(Shader.Find("Standard"));
        //SaveMeshToPath(newMesh, "testmesh");
        //mf.mesh = LoadMeshFromPath("testmesh");
    }

    private void LoadWall(string filename="testmesh")
    {
        GameObject obj = new GameObject("LoadedWall");
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        Mesh newMesh = new Mesh();

        newMesh = LoadMeshFromPath(filename);

        newMesh.RecalculateBounds();

        mf.mesh = newMesh;
        mr.material = new Material(Shader.Find("Standard"));
    }

    private void SaveMeshToPath(Mesh mesh, string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);
        byte[] bytes = MeshSerializer.WriteMesh(mesh, true);
        File.WriteAllBytes(fullPath, bytes);
    }
    private Mesh LoadMeshFromPath(string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(fullPath) == true)
        {
            byte[] bytes = File.ReadAllBytes(fullPath);
            Debug.Log("Load the mesh successfully from >> " + fullPath);
            return MeshSerializer.ReadMesh(bytes);
        }
        return null;
    }

    private void ReviseIndex()
    {
        Vector3 center = (points[0] + points[1] + points[2] + points[3]) / 4;
        Vector3[] lines = new Vector3[72];
        for (int i = 0; i < 72; i++)
            lines[i] = new Vector3(Mathf.Cos(i * Mathf.PI / 36), 0, Mathf.Sin(i * Mathf.PI / 36));

        int[] max = { 0, 0, 0, 0 };
        float[,] innerProduct = new float[72, 4];

        for (int i = 0; i < 72; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                innerProduct[i, j] = (points[j].x - center.x) * lines[i].x + (points[j].z - center.z) * lines[i].z;
                if (innerProduct[max[j], j] < innerProduct[i, j]) max[j] = i;
            }
        }

        Dictionary<int, int> sort = new Dictionary<int, int>();
        sort.Add(max[0], 0); sort.Add(max[1], 1); sort.Add(max[2], 2); sort.Add(max[3], 3);
        
        Array.Sort(max);
        Array.Reverse(max);
        Vector3[] tempPoints = new Vector3[4];
        for (int i = 0; i < 4; i++) tempPoints[i] = points[sort[max[i]]];
        for (int i = 0; i < 4; i++) points[i] = tempPoints[i];
    }

}