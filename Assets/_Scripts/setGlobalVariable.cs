using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setGlobalVariable : MonoBehaviour {

    public void setSelect(int select) {
        GlobalVariable.select = select;
    }

    public void setWidth(Text text) {
        float width;
        if (float.TryParse(text.text, out width))
            GlobalVariable.width = float.Parse(text.text);
        else
            GlobalVariable.width = 4.0f;
    }

    public void setLength(Text text) {
        float length;
        if (float.TryParse(text.text, out length))
            GlobalVariable.length = float.Parse(text.text);
        else
            GlobalVariable.length = 6.0f;
    }
}
