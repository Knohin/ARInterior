using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoom : MonoBehaviour {
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
    public Text mytext;
    public Text mytext2;
    Ray ray;
    // Use this for initialization
	void Start () {
    //    tar = GameObject.Find("ImageTarget");
        mytext = GameObject.Find("testtext").GetComponent<Text>();
        mytext.text = "first setting";
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.touchCount >= 2)// Zoomin, Zoomout
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
                mytext2.text = tempstr;
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
        mytext.text = max.ToString();
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

        for (tempN = 10000.0f; tempN > 1; tempN/=10.0f)
        {
            if (O_scale / tempN >= 1)
            {
                return tempN/5.0f;
            }
        }
        return tempN;
    }

}
