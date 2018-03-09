using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterplane : MonoBehaviour {

    private static masterplane s_instance = null;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public static masterplane instance
    {
        get
        {
            if(s_instance == null)
            {
                s_instance = new GameObject("Masterplane").AddComponent<masterplane>();
                //if object not loaded creating object
            }
            return s_instance;
        }
    }

    void OnApplicationQuit()
    {
        s_instance = null;
    }
}
