using UnityEngine;

namespace myMath
{
    public class common_math
    {
        public static float getDistance(Vector3 v1, Vector3 v2)
        {
            float subx = v1.x - v2.x;
            float suby = v1.y - v2.y;
            float subz = v1.z - v2.z;

            return Mathf.Sqrt(Mathf.Pow(subx, 2) + Mathf.Pow(suby, 2) + Mathf.Pow(subz, 2));
        }
    }
}
