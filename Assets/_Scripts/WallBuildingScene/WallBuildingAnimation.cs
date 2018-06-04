/* 
 * 설명 :
 *  클릭(혹은 터치) 를 이용해 4개의 점을 찍으면 그 점을 꼭지점으로 하는 사각형의 벽이 만들어진다.
 * 
 * 사용법 : 
 *  아무 GameObject에 붙이면 된다.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vuforia;

public class WallBuildingAnimation : MonoBehaviour
{
    [HideInInspector] public float Height;
    public float Thickness;
    public Texture WallTexture;
    [Space(10)]
    public GameObject SandStorm;
    
    private GameObject wall;

    private void Update()
    {
    }

    public void ReviseIndex(Vector3[] points)
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
    public GameObject BuildWall(Vector3[] points)
    {
        Vector3 HeightVector = Vector3.up * Height;
        wall = new GameObject("Wall");
        GameObject interiorGroup = GameObject.Find("Interior Group");
        if (interiorGroup == null)
            interiorGroup = new GameObject("Interior Group");
        wall.transform.parent = interiorGroup.transform;

        MeshFilter mf = wall.AddComponent<MeshFilter>();
        MeshRenderer mr = wall.AddComponent<MeshRenderer>();
        wall.AddComponent<SerializedObject>();

        Mesh MeshToSave = new Mesh();
        mf.mesh = MeshToSave;

        // Set Vertices (Normals and UVs)
        {
            // 내부 벽
            List<Vector3> vertices = new List<Vector3>();
            vertices.AddRange(new Vector3[] {
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
            for (int i = 0; i < 16; i += 4)
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
                Vector3 distance = Quaternion.Euler(Vector3.down * 180 * theta / Mathf.PI) * -normals[i * 4];
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

            MeshToSave.SetVertices(vertices);
            MeshToSave.SetNormals(normals);

            MeshToSave.triangles = new int[] {
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

            // UV 설정
            {
                float[] WallsWidth = new float[8];
                Vector3[] pnt = mf.mesh.vertices;

                for (int i = 0; i < 8; i++)
                {
                    if (i == 3)
                        WallsWidth[i] = (pnt[0] - pnt[12]).magnitude;
                    else if (i == 7)
                        WallsWidth[i] = (pnt[16] - pnt[28]).magnitude;
                    else
                        WallsWidth[i] = (pnt[i * 4 + 2] - pnt[i * 4]).magnitude;
                }

                // Set Texture UV
                Vector2[] uv = new Vector2[pnt.Length];
                for (int i = 0; i < 8; i++)
                {
                    uv[i * 4] = new Vector2(0, 0);
                    uv[i * 4 + 1] = new Vector2(0, Height);
                    uv[i * 4 + 2] = new Vector2(WallsWidth[i], 0);
                    uv[i * 4 + 3] = new Vector2(WallsWidth[i], Height);
                }
                mf.mesh.uv = uv;
            }

            MeshToSave.RecalculateBounds();
            //newMesh.RecalculateNormals();
            //newMesh.RecalculateTangents();
        }
        // Set Texture of Material
        mr.material = new Material(Shader.Find("Standard"));
        mr.material.mainTexture = WallTexture;

        // Remove Child Object
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);

        // Play Building Animation
        //StartCoroutine("AnimateWallRising");

        return wall;
    }
    private IEnumerator AnimateWallRising()
    {
        const float RunTime = 4.0f;
        float elapsedTime = 0.0f;
        int[] indexOfFloor = new int[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
        int[] indexOfCeiling = new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 32, 33, 34, 35, 36, 37, 38, 39 };

        Mesh mesh = wall.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;

        foreach (int i in indexOfFloor)
            uvs[i] += Vector2.up * Height;
        foreach (int i in indexOfCeiling)
            vertices[i] -= Vector3.up * Height;

        // Set Sand Particles
        int[] indexOfOuterFloor = new int[] { 16, 18, 20, 22, 24, 26, 28, 30 };
        Transform sandStormTr = SandStorm.transform;
        GameObject sand = sandStormTr.GetChild(0).gameObject;
        for (int i = 0; i < indexOfOuterFloor.Length; i += 2)
        {
            Vector3 a = vertices[indexOfOuterFloor[i]];
            Vector3 b = vertices[indexOfOuterFloor[i + 1]];
            float distance = (b - a).magnitude;
            for (float f = 0; f < distance; f += 0.04f)
            {
                float t = f / distance;
                Vector3 delta = a * (1 - t) + b * t;
                Instantiate(sand, delta, sand.transform.rotation, sandStormTr);
            }
        }

        SandStorm.SetActive(true);
        while (elapsedTime < RunTime)
        {
            //Vector3 delta = Vector3.up * Height * Time.deltaTime / RunTime;
            foreach (int i in indexOfFloor)
                uvs[i] -= Vector2.up * Height * Time.deltaTime / RunTime;
            foreach (int i in indexOfCeiling)
                vertices[i] += Vector3.up * Height * Time.deltaTime / RunTime;

            mesh.uv = uvs;
            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = sandStormTr.childCount - 1; 0 < i; --i)
            Destroy(sandStormTr.GetChild(i).gameObject);
        SandStorm.SetActive(false);
        
        yield return null;
    }
}