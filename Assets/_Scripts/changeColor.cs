using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour {

    private Material[] colors;
    private int colorState;

	// Use this for initialization
	public void Start () {
        colors = gameObject.GetComponent<MeshRenderer>().materials;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetColor() {
        return colorState;
    }

    public void SetColor(int colorState) {
        this.colorState = colorState;
        Change();
    }

    public void Change() {
        if (gameObject.name == "bed")
        {
            Debug.Log(transform.Find("blanket").GetComponent<MeshRenderer>().material);
            
            transform.Find("blanket").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "desk") {
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("drawer1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("drawer2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg4").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "deskChair") {
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg4").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("back").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "wardrobe") {
            transform.Find("animation1").Find("door1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("animation2").Find("door2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("drawer").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("ceiling").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "bookshelf") {
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor4").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor5").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "table") {
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("leg4").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall4").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "tableChair")
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
        else if (gameObject.name == "refrigerator") {
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("animation").Find("door2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("floor3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("bottom").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "sink") {
            transform.Find("base").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table4").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "gasrange")
        {
            transform.Find("base").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("table4").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "oven") {
            transform.Find("base").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door3").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("door4").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("space1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("space2").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "television") {
            transform.Find("drawer1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("drawer1").Find("door").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("drawer2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("drawer2").Find("door").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall").Find("floor1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall").Find("floor2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall").Find("floor3").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "sofa") {
            transform.Find("body").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("matrix").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wall").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wing1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("wing2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("back1").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("back2").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("back3").GetComponent<MeshRenderer>().material = colors[colorState];
        }
        else if (gameObject.name == "lightStand") {
            transform.Find("top").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("bottom").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("pillar").GetComponent<MeshRenderer>().material = colors[colorState];
            transform.Find("prop").GetComponent<MeshRenderer>().material = colors[colorState];
        }
    }
}
