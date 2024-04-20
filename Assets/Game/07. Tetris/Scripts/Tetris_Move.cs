using System.Collections;
using UnityEngine;
using LogicBase;

public class Tetris_Move : MonoBehaviour
{
    Tetris_GameManager GameManager;
    Tetris_UI UI;
    Logic2D logic;
    Tetris_Map Map;

    public float Auto_Speed = 1f;
    public float Down_Delay = 0.1f;
    public float RL_Dleay = 0.1f;

    public bool RL_Move = true;

    void Start()
    {
        GameManager = GetComponent<Tetris_GameManager>();
        Map = GetComponent<Tetris_Map>();
        logic = new Logic2D();
        UI = GetComponent<Tetris_UI>();
    }


    // 게임 시작
    public void GameStart()
    {
        StartCoroutine(Auto_Down());
    }

    public void GameStop()
    {
        StopAllCoroutines();
    }




    // 자동 다운

    public IEnumerator Auto_Down()
    {
        yield return new WaitForSeconds(Auto_Speed);

        GameManager.Down();
        StartCoroutine(Auto_Down());
    }

    public void NextLevel()
    {
        Auto_Speed /= 2;
        Down_Delay /= 2;
    }
        


    public IEnumerator Down_Delay_On()
    {
        yield return new WaitForSeconds(Down_Delay);
        GameManager.DownDleay = false;
    }


    IEnumerator RL_Down_Dleay_On()
    {
        yield return new WaitForSeconds(RL_Dleay);
        RL_Move = true;
    }









    void Update()
    {
        if (GameManager.GameOver || UI.StopGame || !GameManager.StartCheck)
        {
            StopAllCoroutines();
            return; // 게임이 종료면 실행하지 않는다.
        }

        if (Input.GetKeyDown(KeyCode.Z)) GameManager.ChangeForm(); // z키를 누르면 형태를 변경한다.


        if (Input.GetKey(KeyCode.DownArrow) && !GameManager.Down_Max && !GameManager.DownDleay) // 아래키를 누르고 있고 , 다음 밑칸이 존재 하고 , 딜레이가 없다면 
        {
            StopAllCoroutines(); // 모든 코루틴을 정지하고
            GameManager.Down(); // 현재 위치값을 아래로 내리는 함수를 실행한다.
            RL_Move = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            StartCoroutine(Auto_Down());
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Max_Down();
        }



        if (Input.GetKey(KeyCode.RightArrow) && RL_Move) // 오른쪽으로 움직입니다.
        {
            RL_Move = false;
            GameManager.ArrSet(0);

            if (GameManager.MapEmpty((int)Logic2D.Direction.R))
            {
                GameManager.RL_Move(0, 1, 1);
                if (!GameManager.GameOver) Map.Block_Reset();
            }

            else GameManager.ArrSet(1);

            StartCoroutine(RL_Down_Dleay_On());
            
        }


        if (Input.GetKey(KeyCode.LeftArrow) && RL_Move) // 왼쪽으로 움직입니다.
        {
            RL_Move = false;
            GameManager.ArrSet(0);

            if (GameManager.MapEmpty((int)Logic2D.Direction.L))
            {
                GameManager.RL_Move(0, -1, 1);
                if (!GameManager.GameOver) Map.Block_Reset();
            }

            else GameManager.ArrSet(1);

            StartCoroutine(RL_Down_Dleay_On());

        }





    }

}
