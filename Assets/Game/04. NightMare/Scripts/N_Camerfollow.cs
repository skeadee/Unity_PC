using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Camerfollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f; // 카메라가 쫒아가는 속도


    Vector3 offset;

     void Start()
    {
        offset = transform.position - target.position;
      
    }

    void FixedUpdate()
    {
         Vector3 newPos = target.position + offset;
       
        transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);
    }
}
