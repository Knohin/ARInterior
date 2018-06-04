using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice {
    public class drawOutline : MonoBehaviour {

        private Outline[] outlines;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter(Collider col) {
            outlines = GetComponentsInChildren<Outline>();
            foreach (Outline outline in outlines)
                outline.enabled = true;
        }

        /*
        void OnTriggerStay(Collider col) {
            foreach (Outline outline in outlines)
                outline.enabled = true;
        }
        */

        void OnTriggerExit(Collider col)
        {
            foreach (Outline outline in outlines)
                outline.enabled = false;
        }
    }
}

