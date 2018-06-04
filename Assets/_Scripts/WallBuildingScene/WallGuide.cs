
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vuforia;

public class WallGuide : MonoBehaviour
{
    public GameObject AnchorStage;
    [Space(10)]
    public float Height;
    public Material GuideWallMaterial;
    public GameObject GuideMarkPrefab;
    [Space(10)]
    public GameObject SaveMenu;

    private int touchCount;
    private Vector3[] points;
    private GameObject GuideWall;
    private Mesh GuideWallMesh;

    private Transform GuideMarkTr;
    
    private GameObject wall;

    private WallBuildingAnimation buildingAnim;
    //private Mesh MeshToSave;

    private void Start()
    {
        if (GuideMarkPrefab == null || SaveMenu == null)
            Debug.LogError("Check if any public member variable is null!");
        GuideMarkTr = Instantiate(GuideMarkPrefab, Vector3.zero, GuideMarkPrefab.transform.rotation).transform;
        GuideMarkTr.parent = AnchorStage.transform;

        buildingAnim = GetComponent<WallBuildingAnimation>();
        buildingAnim.Height = Height;

        Initialize();
    }

    private void Update()
    {
        Vector2 centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("Plane")))
        {
            GuideMarkTr.position = hit.point;
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
                GuideWall = InstantiateGuideWall(hit.point);
                GuideWall.transform.parent = this.transform;
                GuideWallMesh = GuideWall.GetComponent<MeshFilter>().mesh;

                touchCount++;
                if (touchCount == 4)
                {
                    if (buildingAnim.enabled)
                    {
                        buildingAnim.ReviseIndex(points);
                        buildingAnim.BuildWall(points);
                    }
                }
            }
        }
    }

    public void Initialize()
    {
        touchCount = 0;
        points = new Vector3[4];
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
        if (touchCount == 0 )
            return;

        Vector3[] vertices = GuideWallMesh.vertices;

        if (touchCount == 4)
        {
            vertices[2] = vertices[6] = points[0];
            vertices[3] = vertices[7] = points[0] + Vector3.up * Height;
            touchCount = 0;
        }
        else
        {
            vertices[2] = vertices[6] = hitPoint;
            vertices[3] = vertices[7] = hitPoint + Vector3.up * Height;
        }
        GuideWallMesh.vertices = vertices;
        GuideWallMesh.RecalculateBounds();

        // Normal 설정
        //Vector3 norm = Vector3.Cross(GuideWallMesh.vertices[1] - GuideWallMesh.vertices[0], GuideWallMesh.vertices[2]- GuideWallMesh.vertices[0]).normalized;
        //GuideWallMesh.normals = new Vector3[] {norm, norm, norm, norm, -norm, -norm, -norm, -norm};
        GuideWallMesh.RecalculateNormals();

        // UV 설정
        float width = (GuideWallMesh.vertices[0] - GuideWallMesh.vertices[2]).magnitude;
        GuideWallMesh.uv = new Vector2[] {
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
    
    private GameObject InstantiateMarkAtChild(Vector3 pos)
    {
        GameObject mark = Instantiate(GuideMarkPrefab, pos, GuideMarkPrefab.transform.rotation);
        mark.transform.SetParent(this.transform);

        return mark;
    }
    private GameObject InstantiateGuideWall(Vector3 pos)
    {
        // If it touched, Fix the guideWall and append to this
        GameObject guideWall = new GameObject("GuideWall");
        MeshFilter mf = guideWall.AddComponent<MeshFilter>();
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

        MeshRenderer mr = guideWall.AddComponent<MeshRenderer>();
        //mr.material = new Material(GuideWallMaterial);
        mr.material = GuideWallMaterial;

        return guideWall;
    }

    public void SaveWall(string fileNameToSave = "testmesh")
    {
        MeshSerializer.SaveMeshToPath(wall.GetComponent<MeshFilter>().mesh, fileNameToSave);
        Initialize();
    }
    public static GameObject LoadWall(string fileNameToLoad, string newWallName = "Wall")
    {
        GameObject obj = new GameObject(newWallName);
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        Mesh newMesh = new Mesh();

        newMesh = MeshSerializer.LoadMeshFromPath(fileNameToLoad);

        newMesh.RecalculateBounds();

        mf.mesh = newMesh;
        mr.material = new Material(Shader.Find("Standard"));
        //mr.material.mainTexture = WallTexture;

        return obj;
    }
}