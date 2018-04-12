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

public class WallBuilder : MonoBehaviour {

    public float Height = 1.0f;
    public float Thickness = 0.1f;
    public static Texture WallTexture;
    [Space(10)]
    public Material GuideWallMaterial;
    public GameObject GuideMarkPrefab;
    public GameObject GuideGridPrefab;
    [Space(10)]
    public GameObject SaveMenu;

    private int touchCount;
    private Vector3[] points;
    private GameObject GuideWall;
    private Transform GuideMarkTr;
    private GameObject GuideGrid;

    private GameObject wall;

    private Mesh MeshToSave;

    private void Start()
    {
        if (GuideMarkPrefab == null || SaveMenu == null || GuideGridPrefab == null)
            Debug.LogError("Check if any public member variable is null!");
        GuideMarkTr = Instantiate(GuideMarkPrefab, Vector3.zero, GuideMarkPrefab.transform.rotation).transform;
        GuideGrid = Instantiate(GuideGridPrefab, Vector3.zero, GuideGridPrefab.transform.rotation);

        Initialize();

        Debug.Log(Application.dataPath);
        Debug.Log(Application.streamingAssetsPath);
        Debug.Log(Application.persistentDataPath);
    }

    private void Update()
    {
        Vector2 centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GuideMarkTr.position = hit.point;
            GuideGrid.transform.position = hit.point + Vector3.up*0.0001f;
            GuideGrid.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(hit.point.x, hit.point.z);
            UpdateGuideWall(hit.point);

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
#else
            if (0 < Input.touchCount
                && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;
#endif
                points[touchCount] = hit.point;
                InstantiateMarkAtChild(hit.point);
                InstantiateGuideWallAtChild(hit.point);

                touchCount++;
                if (touchCount == 4)
                {
                    ReviseIndex();
                    BuildWall();
                }
            }
        }

    }

    public void Initialize()
    {
        touchCount = 0;
        points = new Vector3[4];
        MeshToSave = null;
        if (wall != null)
        {
            Destroy(wall);
            wall = null;
        }

        // Remove Child Object
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);
    }
    public void UpdateGuideWall(Vector3 hitPoint)
    {
        if (touchCount == 0 || touchCount == 4)
            return;

        Mesh mesh = GuideWall.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = GuideWall.GetComponent<MeshFilter>().mesh.vertices;
        vertices[2] = vertices[6] = hitPoint;
        vertices[3] = vertices[7] = hitPoint + Vector3.up * Height;
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        // Normal 설정
        //Vector3 norm = Vector3.Cross(mesh.vertices[1] - mesh.vertices[0], mesh.vertices[2]- mesh.vertices[0]).normalized;
        //mesh.normals = new Vector3[] {norm, norm, norm, norm, -norm, -norm, -norm, -norm};
        mesh.RecalculateNormals();

        // UV 설정
        float width = (mesh.vertices[0] - mesh.vertices[2]).magnitude;
        mesh.uv = new Vector2[] {
            new Vector2(0,0),
            new Vector2(0,Height),
            new Vector2(width,0),
            new Vector2(width,Height),
            new Vector2(0,0),
            new Vector2(0,Height),
            new Vector2(width,0),
            new Vector2(width,Height)
        };
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
    private GameObject InstantiateMarkAtChild(Vector3 pos)
    {
        GameObject mark = Instantiate(GuideMarkPrefab, pos, GuideMarkPrefab.transform.rotation);
        mark.transform.SetParent(this.transform);

        return mark;
    }
    private GameObject InstantiateGuideWallAtChild(Vector3 pos)
    {
        // If it touched, Fix the guideWall and append to this
        GuideWall = new GameObject("GuideWall");
        GuideWall.transform.parent = this.transform;
        MeshFilter mf = GuideWall.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();
        mf.mesh.vertices = new Vector3[] {
            pos, pos + Vector3.up * Height,
            pos, pos + Vector3.up * Height,
            pos, pos + Vector3.up * Height,
            pos, pos + Vector3.up * Height
        };
        mf.mesh.triangles = new int[] {
            0,1,2,
            1,3,2,
            4,6,5,
            5,6,7
        };

        MeshRenderer mr = GuideWall.AddComponent<MeshRenderer>();
        //mr.material = new Material(GuideWallMaterial);
        mr.material = GuideWallMaterial;

        return GuideWall;
    }
    private void BuildWall()
    {
        Vector3 HeightVector = Vector3.up * Height;
        wall = new GameObject("Wall");
        MeshFilter mf = wall.AddComponent<MeshFilter>();
        MeshRenderer mr = wall.AddComponent<MeshRenderer>();

        MeshToSave = new Mesh();
        mf.mesh = MeshToSave;

        // Set Vertices (Normals and UVs)
        {
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
                Vector3[] points = mf.mesh.vertices;

                for (int i = 0; i < 8; i++)
                {
                    if (i == 3)
                        WallsWidth[i] = (points[0] - points[12]).magnitude;
                    else if (i == 7)
                        WallsWidth[i] = (points[16] - points[28]).magnitude;
                    else
                        WallsWidth[i] = (points[i * 4 + 2] - points[i * 4]).magnitude;
                }

                // Set Texture UV
                Vector2[] uv = new Vector2[points.Length];
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
        StartCoroutine("AnimateWallRising");
    }
    IEnumerator AnimateWallRising()
    {
        const float RunTime = 2.0f;
        float elapsedTime = 0.0f;
        int[] indexOfLower = new int[] {0,2,4,6,8,10,12,14,16,18,20,22,24,26,28,30 };
        int[] indexOfUpper = new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 32, 33, 34, 35, 36, 37, 38, 39 };

        Mesh mesh = wall.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = mesh.uv;

        foreach (int i in indexOfLower)
            uvs[i] += Vector2.up * Height;
        foreach (int i in indexOfUpper)
            vertices[i] -= Vector3.up * Height;

        while (elapsedTime < RunTime)
        {
            //Vector3 delta = Vector3.up * Height * Time.deltaTime / RunTime;
            foreach (int i in indexOfLower)
                uvs[i] -= Vector2.up * Height * Time.deltaTime / RunTime;
            foreach (int i in indexOfUpper)
                vertices[i] += Vector3.up * Height * Time.deltaTime / RunTime;

            mesh.uv = uvs;
            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // After animated, Show 'Save Menu'
        SaveMenu.SetActive(true);
        yield return null;
    }
    

    public void SaveWall(string fileNameToSave = "testmesh")
    {
        MeshSerializer.SaveMeshToPath(MeshToSave, fileNameToSave);
        Initialize();
    }
    public static GameObject LoadWall(string fileNameToLoad, string newWallName = "LoadedWall")
    {
        GameObject obj = new GameObject(newWallName);
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        Mesh newMesh = new Mesh();

        newMesh = MeshSerializer.LoadMeshFromPath(fileNameToLoad);

        newMesh.RecalculateBounds();

        mf.mesh = newMesh;
        mr.material = new Material(Shader.Find("Standard"));
        mr.material.mainTexture = WallTexture;

        return obj;
    }
}