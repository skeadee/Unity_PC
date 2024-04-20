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

        float h = Input.GetAxisRaw("Horizontal"); // ��:-1 , ��:1
        float v = Input.GetAxisRaw("Vertical"); //��:-1 , ��:1

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

        // �������� �Ǵ� ������ , �������� ������� ������ ������ ������Ʈ , �������� ���� , � ���̾� ����ũ�� ������ ���ΰ� 
        if (Physics.Raycast(camRay, out floorHit, camRayLength , floorMask)) 
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newPos = Quaternion.LookRotation(playerToMouse);
            playerRb.MoveRotation(newPos);
        }

    }

    void Move(float h , float v) // 3��Ī �̵����
    {
        // ���� �� ���� ���� vector�� ������ ���Ѵ�
         movement.Set(h, 0f, v);
        
        // ��� ���⿡ ���� ũ�⸦ '1'�� ����� (normalized)
        // verctor�� ũ�⸦ ���Ѵ�
        movement = movement.normalized * speed * Time.deltaTime;

        // vector�� ũ��� ������ ������ �Ǿ����� , ���� �����ӿ��� ��ġ�� ���Ѵ�.
        playerRb.MovePosition(transform.position + movement);
    }

    void Move2(float h, float v) // 1��Ī �̵� ���
    {
        movement = (transform.forward * v + transform.right * h) * speed * Time.deltaTime;

        playerRb.MovePosition(transform.position + movement);
    }


}
