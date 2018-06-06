using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveCheck : MonoBehaviour {

    public Text text;
    public JsonManager jManager;

    public void check() {
        if (GlobalVariable.select != 2) {
            text.fontSize = 60;
            text.text = "방 도면을 선택했을 때만\n가능합니다.";
        }
        else {
            text.fontSize = 162;
            text.text = "저장 완료.";

            jManager.SaveObjectInJson("SaveTable01.json");
        }
    }
}
