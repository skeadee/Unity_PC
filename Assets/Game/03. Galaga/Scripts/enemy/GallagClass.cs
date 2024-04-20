using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagClass : MonoBehaviour
{


   


}

public class Beziermove : MonoBehaviour
{
    public Vector3[] Route;
    BezierCurve2 bezier;
    int i = 0;

    protected IEnumerator bezierMove(GameObject target , int segmentation, float speed) // 경로 이동 
    {
        bezier = new BezierCurve2();

        Vector3[] Routes = new Vector3[segmentation];
        //Routes = bezier.BezierCurveRoute(Route, segmentation);

        for (; i < Routes.Length; i++)
        {
            Routes = bezier.BezierCurveRoute(Route, segmentation);

            target.transform.position = Routes[i];

            if (i + 1 != Routes.Length)
            {
                Vector3 A = Routes[i];
                Vector3 B = Routes[i + 1];
                Vector3 C = A - B;

                float angle = Mathf.Atan2(C.y, C.x) * Mathf.Rad2Deg;

                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }

            if (i == Routes.Length - 1)
            {
                StartCoroutine(orgLoc(target , speed));
            }

            yield return new WaitForSeconds(0.01f * speed);
           
        }

        i = 0;

    }


    IEnumerator orgLoc(GameObject target , float speed)
    {
        float p = 0;

        if (target.transform.rotation.z < 0) p = 2f;
        else if (target.transform.rotation.z > 0) p = -2f;

        while(true)
        {
            if(p == 2f && target.transform.rotation.z > 0) break;
            if (p == -2f && target.transform.rotation.z < 0) break;

            target.transform.Rotate(new Vector3(0, 0, p));
            yield return new WaitForSeconds(0.01f * speed);
        }
       
    }

}

