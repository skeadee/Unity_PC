using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oth_Mouse : MonoBehaviour
{
    Oth_GameManager GameManager;

    AudioSource audioSource;

    Vector3 Start_Pos = new Vector3(-3.16f , 3.16f , 0);
    Vector3 mouse;
    float Add = 0.9f;

    float[] Loc_x = new float[8];
    float[] Loc_y = new float[8];

    public int Arr_x;
    public int Arr_y;

    public GameObject[] Dolls;
    GameObject LastDoll;
    int Last_x;
    int Last_y;

    void Start()
    {
        GameManager = GetComponent<Oth_GameManager>();
        audioSource = GetComponent<AudioSource>();

        Mouse_ArrSet();
        Borad_Reset();
    }

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Arr_LocSet();

        if (GameManager.Map[Arr_y , Arr_x] != 0)    // 3. 마우스 위치값에 값이 들어가 있으면 취소
        {
            Value_Reset(); 
            return;
        }


        if(GameManager.DirCheck(Arr_y, Arr_x)) // 4. 옆에 바꿀수 있는 돌이 있는지 확인인한다
        {
            Doll_Set();

            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                GameManager.ChangeValue(Arr_y , Arr_x);
                audioSource.Play();

                Borad_Reset();
                GameManager.GameEndCheck();

                GameManager.turn = (GameManager.turn == 1) ? 2 : 1;
            } 
                
        }
        
        else
        {
            Value_Reset();
        }

       

      
    }


    void Mouse_ArrSet() // 1. 마우스 배열 세팅
    {
        for (int i = 0; i < 8; i++)
        {
            Loc_x[i] = Start_Pos.x;
            Loc_y[i] = Start_Pos.y;

            Start_Pos.x += Add;
            Start_Pos.y -= Add;
        }
    }

    void Arr_LocSet() // 2. 마우스 위치에 따른 배열 위치 세팅
    {
        for(int i=0;i<8;i++)
        {
            if (Loc_x[i] < mouse.x) Arr_x = i;
            if (Loc_y[i] > mouse.y) Arr_y = i;
        }

    }

    void Doll_Set() // 5. trun 과 위치에 맞게 돌이 보이게 한다
    {
        if (Arr_x == Last_x && Arr_y == Last_y) return;

        if (LastDoll != null) Destroy(LastDoll);


        GameObject doll = (GameManager.turn == 1) ? Dolls[0] : Dolls[1];

        LastDoll = Instantiate(doll, new Vector3(Loc_x[Arr_x] , Loc_y[Arr_y] , 0), Quaternion.identity);

        Last_x = Arr_x;
        Last_y = Arr_y;
    }

    void Borad_Reset()
    {
        GameObject[] Respawn = GameObject.FindGameObjectsWithTag("Respawn");

        for (int i = 0; i < Respawn.Length; i++)
        {
            Destroy(Respawn[i]);
        }

        for(int i=0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            { 
                if (GameManager.Map[i, j] == 1) Instantiate(Dolls[0], new Vector2(Loc_x[j], Loc_y[i]), Quaternion.identity);
                if (GameManager.Map[i, j] == 2) Instantiate(Dolls[1], new Vector2(Loc_x[j], Loc_y[i]), Quaternion.identity);
            }
        }

    }

    void Value_Reset()
    {
        if (LastDoll != null) Destroy(LastDoll);
        Last_x = -1;
        Last_y = -1;
    }


}
