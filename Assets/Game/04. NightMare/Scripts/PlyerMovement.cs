using UnityEngine;
using System;

public class PlyerMovement : MonoBehaviour
{
    N_CameraChange camerachange;
    N_GameManager GameManager;
    N_PlayerHealth playerHealth;

    public float speed = 6f;
    public Rigidbody playerRb;
    public Animator anim;

    Vector3 movement;
    int floorMask;
    float camRayLength = 100f;

    public bool TurningOn = true;

    void Awake()
    {
        playerHealth = GetComponent<N_PlayerHealth>();
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
        floorMask = LayerMask.GetMask("Floor");
        camerachange = GetComponent<N_CameraChange>();
    }

    void FixedUpdate()
    {
        if (GameManager.GameStop) return;

        float h = Input.GetAxisRaw("Horizontal"); // 좌:-1 , 우:1
        float v = Input.GetAxisRaw("Vertical"); //하:-1 , 상:1

        if(!camerachange.change) Move(h,v); 
        else if(camerachange.change) Move2(h, v);


        if (TurningOn) Turning();
        Animating(h, v);
    }


    void Animating(float h , float v)
    {
        bool walking = (h != 0f) || (v != 0f);
        anim.SetBool("Isworking", walking);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); 

        RaycastHit floorHit;

        // 기준점이 되는 레이져 , 레이저가 닿았을때 정보를 저장할 오브젝트 , 레이져의 길이 , 어떤 레이어 마스크를 감지할 것인가 
        if (Physics.Raycast(camRay, out floorHit, camRayLength , floorMask)) 
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newPos = Quaternion.LookRotation(playerToMouse);
            playerRb.MoveRotation(newPos);
        }

    }

    void Move(float h , float v) // 3인칭 이동방법
    {
        // 전달 된 값에 따라서 vector의 방향을 정한다
         movement.Set(h, 0f, v);
        
        // 모든 방향에 대해 크기를 '1'로 만들고 (normalized)
        // verctor의 크기를 정한다
        movement = movement.normalized * speed * Time.deltaTime;

        // vector의 크기와 방향이 결정이 되었으니 , 다음 프레임에서 위치를 정한다.
        playerRb.MovePosition(transform.position + movement);
    }

    void Move2(float h, float v) // 1인칭 이동 방법
    {
        movement = (transform.forward * v + transform.right * h) * speed * Time.deltaTime;

        playerRb.MovePosition(transform.position + movement);
    }


}
