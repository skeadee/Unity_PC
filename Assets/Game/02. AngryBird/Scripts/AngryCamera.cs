using UnityEngine;
using System.Collections;


public class AngryCamera : MonoBehaviour
{
    AngryGameMananger angry;

    Camera mainCamera;
    GameObject ball;

    Vector3 loc = new Vector3(0,0,-10);
    Vector3 cam;

    public float CameraSpeed;
    public string BallName;    

    public int GameMode;

    // GameMode = 0 카메라가 오른쪽으로 이동하기
    // GameMode = 1 카메라가 대기 시간을 가진다
    // GameMode = 2 카메라가 왼쪽으로 이동하기
    // GameMode = 3 카메라 종료

    float t;  // 카메라 대기 시간
    

    void Start()
    {
        angry = GameObject.Find("AngryGamaManager").GetComponent<AngryGameMananger>();
        mainCamera = Camera.main;
        ball = GameObject.Find(BallName);

        t = 2f;
    }

    void Update()
    {
        if (angry.GameMode == 0)
        {
            mainCamera.transform.position = new Vector3(0, 0, -10);
            GameMode = 0;
        }
    }

    void FixedUpdate()
    {
        if (angry.GameMode == 2 || angry.GameMode == 3) return;

        else if (angry.GameMode == 1)
        {
            if (GameMode == 0) Right();
            else if (GameMode == 1) Stop();
            else if (GameMode == 2) Left();
        }

        BallCamera();
    }

    void Right()
    {
        if (loc.x <= 30) loc.x += Time.deltaTime * CameraSpeed;
        else GameMode = 1;

        mainCamera.transform.position = loc;
    }

    void Stop()
    {
        if (t > 0) t -= Time.deltaTime;

        else
        {
            GameMode = 2;
            t = 2f;
        }
    }

    void Left()
    {
        if (loc.x >= 0.5f) loc.x -= Time.deltaTime * CameraSpeed;
        else GameMode = 3;

        mainCamera.transform.position = loc;
    }

    void BallCamera()
    {
        if (ball == null)
        {
            ball = GameObject.Find(BallName);
        }

        if (ball.transform.position.x > 5)
        {
            cam = ball.transform.position;
            cam.z = -10;
            cam.y = 0;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cam, Time.deltaTime * CameraSpeed);
        }
    }












}
