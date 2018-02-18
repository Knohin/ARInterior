using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseSelector : MonoBehaviour {
    
    public GameObject[] HouseModels;
    public Dropdown dropdown;

    private GameObject mTargetHouse;

    private void Start()
    {
        mTargetHouse = GameObject.Instantiate(HouseModels[dropdown.value], Vector3.zero, Quaternion.identity);
    }

    public void selected()
    {
        GameObject.Destroy(mTargetHouse);
        mTargetHouse = GameObject.Instantiate(HouseModels[dropdown.value], Vector3.zero, Quaternion.identity);
    }
}
