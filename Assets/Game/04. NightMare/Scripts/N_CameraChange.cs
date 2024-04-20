using UnityEngine;
using UnityEngine.UI;

public class N_CameraChange : MonoBehaviour
{
    Transform playerCam;
    GameObject camObj;
    N_GameManager GameManager;

    public GameObject tragetImg;
    public bool change = false; // 현재 1인칭인지 3인칭인지 체크하는 변수
    public int Sensitivity = 1; // 마우스 감도 조절 변수

    float x, y;

    N_AutoButton autobutton;
    public Slider slider; // 마우스 감도 조절 용 슬라이더


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
        if (GameManager.GameStop) // 게임이 정지 상태면 실행중지 하기 
        {
            Cursor.visible = true; // 마우스를 활성화하고 
            Cursor.lockState = CursorLockMode.None; // 마우스 제한을 푼다

            return;
        }


       MouseCheck();
       Change();

       if(camObj.activeSelf == true) Mouse();
    }


    void MouseCheck()
    {
        if(change) // 1인칭일 경우
        {
            Cursor.visible = false; // 마우스 비활성화 
            Cursor.lockState = CursorLockMode.Locked; // 마우스 고정 
        }

        else if(!change) // 3인칭일 경우
        {
            Cursor.visible = true; // 마우스 활성화
            Cursor.lockState = CursorLockMode.None; // 마우스 고정 해제
        }
    }

    void Change()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (camObj.activeSelf == false) // 1인칭 활성화
            {
                GetComponent<Animator>().enabled = false;

                

                tragetImg.SetActive(true);
                change = true;
                camObj.SetActive(true);
                autobutton.AutoEnd(); // 1인칭일 경우에는 오토모드를 종료한다.
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

        x = Mathf.Clamp(x, -30f , 10f); // 높이 조정
        
        transform.rotation = Quaternion.Euler(x, y, 0);
    }

    


}
