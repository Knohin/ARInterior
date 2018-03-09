using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom_3d : MonoBehaviour {
    Vector2 prevPos;
    Vector2 nowPos;
    Vector3 movePos;

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    float TouchData;
    Vector2 cur;
    Vector2 Prev;
    public Camera ca;   //main camera
    public GameObject tar;
    Ray ray;

    // Use this for initialization
    void Start () {
		
	}
	//zoom기능의 경우 sight의 angle을 조절하여 zoom 기능을 구현.
    //원하는 곳으로의 zoom의 경우 phase의 began에 해당하는 두좌표를 받아 중점을 구함.(ray 또한 구함)
    //해당 중심을 기기 중심으로 옮기는 과정을 일정 속도로 실시 -> pointToDisplay통해 display에 해당하는
    //해당 중심, 기기 중심의 거리의 차를 이용하여 서서히 천천히 중심으로 다가오도록 설계 -> 확대의 속도 상이
    //해당 중심을 기기 중심으로 옮기는 과정에서 카메라의 전방방향의 회전각도를 조절하도록 설계하여야함.
    //즉, 위 내용 구현시에 해당 중심, 기기 중심간의 화면상의 y축거리를 구함 -> (y축의 차)/(default속도)
    //식을 통하여 카메라의 전방방향 회전각도를 변동 시키며, 이는 총 회전가능한 회전각도에서 비율상 구한다.
    
    //이를 위해서는 가장 첫 카메라의 rotation을 기억하고 있어야함.-> 다시 되돌려야 하기때문
    //첫 rotation의 pointToDisplay와 지금의 Display상 좌표 차이 통해 

	// Update is called once per frame
	void Update () {
        if (Input.touchCount >= 2)// Zoomin, Zoomout
        {
            cur = Input.GetTouch(0).position - Input.GetTouch(1).position;
            Prev = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition)
                - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); // sub nowPos - prevPos
            TouchData = cur.magnitude - Prev.magnitude;//magnitude calculate sqrt.
            //return Zoomin is 1, Zoomout is -1
            //ca.transform.Translate(0, 0, TouchData * Time.deltaTime * 10.0f);
            Vector3 tempV = tar.transform.localScale;

            if (tempV.x >= 0 && tempV.y >= 0 && tempV.z >= 0)
            {
                string tempstr = tempV.x.ToString() + ", " + tempV.y.ToString() + ", " + tempV.z.ToString();
                float tempf;
                Vector3 addingscale;
                //adding scale
                if (TouchData > 0)
                {
                    tempf = CalScale(JudgeLargeScale(tempV.x, tempV.y, tempV.z), TouchData);
                    addingscale = new Vector3(tempf, tempf, tempf);
                    tar.transform.localScale += addingscale;
                }
                else //sub scale
                {
                    tempf = CalScale(JudgeLargeScale(tempV.x, tempV.y, tempV.z), TouchData);
                    addingscale = new Vector3(tempf, tempf, tempf);
                    tar.transform.localScale -= addingscale;
                }
            }
        }
    }

    float JudgeLargeScale(float px, float py, float pz)
    {
        float max;
        if (px > py)
            max = px;
        else
            max = py;
        if (pz > max)
            max = pz;
        return max;
    }
    
    float CalScale(float O_scale, float TouchData)
    {
        float tempN;
        //if minsize
        if (O_scale <= 10 && TouchData > 0)
            return 0.1f;
        else if (O_scale <= 10 && TouchData <= 0)
            return 0.0f;

        for (tempN = 10000.0f; tempN > 1; tempN /= 10.0f)
        {
            if (O_scale / tempN >= 1)
            {
                return tempN / 5.0f;
            }
        }
        return tempN;
    }

}
