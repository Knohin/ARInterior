using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using myMath;
using Vuforia;

namespace dist_planeToCamera
{
    public class dist_planeTocamera
    {
        public static GameObject image_target;
        public static Camera mainCamera;
        public static GameObject ground;
        public static Text mytext;
        public static Text mytext2;
        public static Text mytext3;

        public ImageTargetBehaviour imgtarget;

        // Use this for initialization
        public void init()
        {
        }

        // Update is called once per frame
        public static float getDist()
        {

            image_target = GameObject.Find("ImageTarget").GetComponent<GameObject>();
            mainCamera = GameObject.Find("ARCamera").GetComponent<Camera>();
            ground = GameObject.Find("Ground_Plane_Stage").GetComponent<GameObject>();
            mytext = GameObject.Find("testtext").GetComponent<Text>();
            mytext2 = GameObject.Find("testtext2").GetComponent<Text>();
            mytext3 = GameObject.Find("testtext3").GetComponent<Text>();

            Vector3 targetP = image_target.transform.position;
            Vector3 cameraP = mainCamera.transform.position;
            mytext.text = (common_math.getDistance(targetP, cameraP)).ToString() + "Cm";
            // mytext.text = (targetP.x - cameraP.x).ToString();

            //mytext.text = imgtarget.GetSize().x.ToString();
            //mytext2.text = imgtarget.GetSize().y.ToString();

            Vector3 targetP2 = ground.transform.position;
            mytext2.text = (common_math.getDistance(targetP2, cameraP)).ToString() + "Cm";
            //mytext2.text = (targetP.y - cameraP.y).ToString();
            mytext3.text = (targetP.z - cameraP.z).ToString();

            return common_math.getDistance(targetP, cameraP);
        }
    }
}
