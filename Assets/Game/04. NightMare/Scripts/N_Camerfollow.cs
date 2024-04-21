using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Camerfollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f; // ī�޶� �i�ư��� �ӵ�


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
