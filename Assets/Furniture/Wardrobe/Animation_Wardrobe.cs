using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Wardrobe : MonoBehaviour {

    private Animator anim;
    bool anima;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anima = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(anima)
        {
            anim.SetBool("animation", true);
            anima = false;
        }
        else
        {
            anim.SetBool("animation", false);
            anima = true;
        }
	}
}
