using UnityEngine;
using UnityEngine.UI;

public class N_CameraChange : MonoBehaviour
{
    Transform playerCam;
    GameObject camObj;
    N_GameManager GameManager;

    public GameObject tragetImg;
    public bool change = false; // ���� 1��Ī���� 3��Ī���� üũ�ϴ� ����
    public int Sensitivity = 1; // ���콺 ���� ���� ����

    float x, y;

    N_AutoButton autobutton;
    public Slider slider; // ���콺 ���� ���� �� �����̴�


    void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
        playerCam = transform.Find("Cam");
        camObj = playerCam.gameObject;
        camObj.SetActive(false);
    }

    void Start()
    {
        autobutton = GameObject.Find("AutoButton").GetComponent<N_AutoButton>();
        tragetImg.SetActive(false);

        x = 0;
        y = 0;
    }

    void Update()
    {
        if (GameManager.GameStop) // ������ ���� ���¸� �������� �ϱ� 
        {
            Cursor.visible = true; // ���콺�� Ȱ��ȭ�ϰ� 
            Cursor.lockState = CursorLockMode.None; // ���콺 ������ Ǭ��

            return;
        }


       MouseCheck();
       Change();

       if(camObj.activeSelf == true) Mouse();
    }


    void MouseCheck()
    {
        if(change) // 1��Ī�� ���
        {
            Cursor.visible = false; // ���콺 ��Ȱ��ȭ 
            Cursor.lockState = CursorLockMode.Locked; // ���콺 ���� 
        }

        else if(!change) // 3��Ī�� ���
        {
            Cursor.visible = true; // ���콺 Ȱ��ȭ
            Cursor.lockState = CursorLockMode.None; // ���콺 ���� ����
        }
    }

    void Change()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (camObj.activeSelf == false) // 1��Ī Ȱ��ȭ
            {
                GetComponent<Animator>().enabled = false;

                

                tragetImg.SetActive(true);
                change = true;
                camObj.SetActive(true);
                autobutton.AutoEnd(); // 1��Ī�� ��쿡�� �����带 �����Ѵ�.
            }

            else
            {
                

                tragetImg.SetActive(false);
                GetComponent<Animator>().enabled = true;
                change = false;
                camObj.SetActive(false);
            }
        }
    }


    void Mouse()
    {
        float Mouse_x = Input.GetAxis("Mouse X");
        float Mouse_y = -Input.GetAxis("Mouse Y");


        y += Mouse_x * Sensitivity * slider.value;
        x += Mouse_y * Sensitivity * slider.value;

        x = Mathf.Clamp(x, -30f , 10f); // ���� ����
        
        transform.rotation = Quaternion.Euler(x, y, 0);
    }

    


}
