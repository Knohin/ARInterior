using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchObject : MonoBehaviour {

    public Text test;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0) {

            Vector2 pos = Input.GetTouch(0).position;
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);

            Ray ray = Camera.main.ScreenPointToRay(theTouch);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    // 터치한 순간

                    //GameObject option = hit.transform.Find("Option").gameObject;
                    //Vector3 direction = gameObject.transform.position - option.transform.position;
                    //test.text = direction.ToString();
                    //option.SetActive(true);

                    //Vector3 direction = gameObject.transform.position - hit.transform.position;
                    //test.text = direction.ToString();

                    GameObject target = hit.collider.gameObject;
                    Transform option = target.transform.Find("Option");
                    BoxCollider[] colliders = target.GetComponents<BoxCollider>();
                    ////Vector3 direction = gameObject.transform.position - target.transform.position;

                    ////test.text = (target.GetComponent<BoxCollider>().transform.position.y * 2 + 1).ToString();
                    //target.GetComponent<Collider>();

                    float width = colliders[0].size.x / 2.0f + 1.0f;
                    float height = colliders[0].center.y + 1.0f;
                    ////float width = target.GetComponent<BoxCollider>().size.x / 2.0f + 1.0f;
                    ////float height = target.GetComponent<BoxCollider>().center.y * 2.0f + 1.0f;

                    //test.text = direction.y.ToString();

                    if (hit.collider == colliders[0])
                    {
                        option.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
                        option.localPosition = new Vector3(0.0f, height, 0.0f);
                        //test.text = "UP";
                    }
                    else
                    {
                        if (height > 30.0f) height -= 10.0f;
                        else if (height > 15.0f) height -= 5.0f;

                        if (hit.collider == colliders[1])
                        {
                            option.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                            option.localPosition = new Vector3(0.0f, height, -10.0f);
                            //test.text = "FORWARD";
                        }
                        else if (hit.collider == colliders[2])
                        {
                            option.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                            option.localPosition = new Vector3(width * -1, height, 0.0f);
                            //test.text = "LEFT";
                        }
                        else if (hit.collider == colliders[3])
                        {
                            option.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
                            option.localPosition = new Vector3(width, height, 0.0f);
                            //test.text = "RIGHT";
                        }
                        else if (hit.collider == colliders[4])
                        {
                            option.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                            option.localPosition = new Vector3(0.0f, height, 10.0f);
                            //test.text = "BACKWARD";
                        }
                    }

                    /*
                    if (direction.y > 75) {
                        option.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
                        option.localPosition = new Vector3(0.0f, height, 0.0f);
                    }
                    else {
                        if (height > 30.0f) height -= 10.0f;
                        else if (height > 12.5f) height -= 5.0f;

                        if (Vector3.Dot(direction, new Vector3(1.0f, 0.0f, -1.0f)) > 0.0f &&
                        Vector3.Dot(direction, new Vector3(-1.0f, 0.0f, -1.0f)) > 0.0f) {
                            option.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                            option.localPosition = new Vector3(0.0f, height, -10.0f);
                        }
                        else if (Vector3.Dot(direction, new Vector3(-1.0f, 0.0f, -1.0f)) > 0.0f &&
                            Vector3.Dot(direction, new Vector3(-1.0f, 0.0f, 1.0f)) > 0.0f) {
                            option.localEulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                            option.localPosition = new Vector3(width * -1, height, 0.0f);
                        }
                        else if (Vector3.Dot(direction, new Vector3(1.0f, 0.0f, 1.0f)) > 0.0f &&
                            Vector3.Dot(direction, new Vector3(1.0f, 0.0f, -1.0f)) > 0.0f) {
                            option.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
                            option.localPosition = new Vector3(width, height, 0.0f);
                        }
                        else if (Vector3.Dot(direction, new Vector3(-1.0f, 0.0f, 1.0f)) > 0.0f &&
                            Vector3.Dot(direction, new Vector3(1.0f, 0.0f, 1.0f)) > 0.0f) {
                            option.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                            option.localPosition = new Vector3(0.0f, height, 10.0f);
                        }
                    }
                    */

                    option.gameObject.SetActive(true);
                    

                    // hit.transform.Find("Option").gameObject.SetActive(true);

                    // hit.transform.Find("Option").gameObject.SetActive(true);

                    /*
                    if (hit.transform.name == "wardrobe")
                    {
                        hit.transform.Find("Option").gameObject.SetActive(true);

                        //var anim = hit.transform.gameObject.GetComponentsInChildren<Animator>();
                        //anim[0].SetTrigger("animation");
                        //anim[1].SetTrigger("animation");
                    }
                    else if (hit.transform.name == "bed") {
                        //hit.transform.FindChild("Canvas").gameObject.SetActive(true);
                        //GameObject option = hit.transform.Find("Option").gameObject;
                        //option.SetActive(true);
                        //Button[] buttons = option.GetComponents<Button>();
                        //Material[] materials = hit.transform.GetComponent<MeshRenderer>().GetComponents<Material>();
                        //buttons[0].colors..normalColor = materials[0].color;

                        hit.transform.Find("Option").gameObject.SetActive(true);// FindChild("Canvas").gameObject.SetActive(true);
                        //if (canvas.gameObject.GetComponent<Renderer>().enabled)
                        //    canvas.gameObject.GetComponent<Renderer>().enabled = true;
                        //else
                        //    canvas.gameObject.GetComponent<Renderer>().enabled = false;
                        //if (canvas.gameObject.activeSelf) canvas.gameObject.SetActive(false);
                        //if (canvas.gameObject.activeInHierarchy) canvas.gameObject.SetActive(false);
                        //else canvas.gameObject.SetActive(true);
                        //canvas.gameObject.SetActive(true);

                        //canvas.gameObject.SetActive(true);
                        //var bed = hit.transform.gameObject;
                        //if (canvas.gameObject.activeInHierarchy) canvas.gameObject.SetActive(true);
                        //else canvas.gameObject.SetActive(false);
                        //canvas.transform.position = hit.transform.position;
                    }
                    else if (hit.transform.name == "refrigerator")
                    {
                        var anim = hit.transform.gameObject.GetComponentInChildren<Animator>();
                        anim.SetTrigger("animation");
                    }
                    else if (hit.transform.name == "sink")
                    {
                        var part = hit.transform.gameObject.GetComponentInChildren<ParticleSystem>();
                        if (part.isPlaying) part.Stop();
                        else part.Play();
                    }
                    else if (hit.transform.name == "gasrange")
                    {
                        var part = hit.transform.gameObject.GetComponentsInChildren<ParticleSystem>();
                        if (part[0].isPlaying)
                        {
                            part[0].Stop();
                            part[1].Stop();
                        }
                        else
                        {
                            part[0].Play();
                            part[1].Play();
                        }
                    }
                    else if (hit.transform.name == "oven")
                    {
                        var part = hit.transform.gameObject.GetComponentInChildren<ParticleSystem>();
                        if (part.isPlaying) part.Stop();
                        else part.Play();
                    }
                    else if (hit.transform.name == "lightStand")
                    {
                        var part = hit.transform.gameObject.GetComponentInChildren<ParticleSystem>();
                        if (part.isPlaying) part.Stop();
                        else part.Play();
                    }
                    */

                }
                /*
                else if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                    // 터치 후 움직임
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                    // 터치 떼면 발생
                }
                */
            }
        }
    }
}
