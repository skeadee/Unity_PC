using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCall : MonoBehaviour
{
    public GameObject bullet;
    public float WaitTime;

    IEnumerator createBullet()
    {
        yield return new WaitForSeconds(WaitTime);


        for(int i=0; i<2;i ++)
        {
            Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, 180));
            yield return new WaitForSeconds(0.7f);
        }
        
    }



    public void CreateBullet()
    { 
        StartCoroutine(createBullet());
    }

}
