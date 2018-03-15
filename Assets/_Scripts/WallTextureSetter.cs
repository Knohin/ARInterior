/*
 * 설명 : 어떤 Mesh의 크기에 알맞게 UV 맵핑을 하고 싶을 때 사용하면,
 *      Start 시점에 MainTextureToSet변수에 세팅된 텍스쳐로 Material을 바꾼다.
 * 
 * 사용법 :
 *  메쉬를 가진 오브젝트에 추가하고, Inspector에서 MainTextureToSet을 정해준다.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTextureSetter : MonoBehaviour {

    public Texture MainTextureToSet;
    public float Height;
    public float[] WallsWidth;

    private void Start()
    {
        // Init member variables
        WallsWidth = new float[8];
        Vector3[] points = GetComponent<MeshFilter>().mesh.vertices;

        Height = (points[1] - points[0]).magnitude;
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
        for (int i=0; i<8; i++)
        {
            uv[i * 4] = new Vector2(0, 0);
            uv[i * 4+1] = new Vector2(0, Height);
            uv[i * 4+2] = new Vector2(WallsWidth[i], 0);
            uv[i * 4+3] = new Vector2(WallsWidth[i], Height);
        }

        GetComponent<MeshFilter>().mesh.uv = uv;

        // Set Texture of Material
        GetComponent<MeshRenderer>().material.mainTexture = MainTextureToSet;
    }
}
