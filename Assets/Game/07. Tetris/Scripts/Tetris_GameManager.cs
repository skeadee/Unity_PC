using UnityEngine;
using LogicBase;

public class Tetris_GameManager : Tetris_Data
{
    Logic2D logic;
    Tetris_Map tetris_Map;
    Tetris_Move Move;
    Tetris_UI UI;
    Tetris_ScoreAni Score_Ani;
    Tetris_Combo Combo;
    AudioSource BackGround_Music;

    public AudioSource Score_Add_Sounrd;
    public int[,] Map;

    int[] Dir = new int[] { (int)Logic2D.Direction.U, (int)Logic2D.Direction.D };
    public bool StartCheck = false;
    [HideInInspector] public bool DownDleay = false;

    public int mode = 0;
    public int count = 0; // 폼 체인지 갯수 체크 하는 변수

    public bool Down_Max;
    int[] NextMode = new int[3];
    int NextModeNumber = -1;
    public bool GameOver = false;

    public int[] y; // 현재 위치 값
    public int[] x;

    public GameObject[] NextBlockPre;

   
    void Awake()
    {
        logic = new Logic2D();
        Application.targetFrameRate = 60;

        Map = new int[18, 10];
    }

    void Start()
    {
        tetris_Map = GetComponent<Tetris_Map>();
        Move = GetComponent<Tetris_Move>();
        UI = GetComponent<Tetris_UI>();
        Score_Ani = GetComponent<Tetris_ScoreAni>();
        Score_Add_Sounrd = GetComponent<AudioSource>();
        Combo = GameObject.Find("Combo_Time").GetComponent<Tetris_Combo>();
        BackGround_Music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        Next_Mode_Set();

        NextBlock(); // 시작시 
        tetris_Map.Block_Reset();
        StartCheck = true;    
    }


    // 다음 블럭을 세팅하는 함수

    int Next_Mode_Set() // 다음 블럭의 순서를 랜덤으로 세팅합니다.
    {
        for (int i = 0; i < NextMode.Length; i++)
        {
            if (NextMode[i] == 0) NextMode[i] = Random.Range(1, 8);
        }

        NextModeNumber = (NextModeNumber + 1) % NextMode.Length;

        int p = NextMode[NextModeNumber];
        NextMode[NextModeNumber] = 0;

        Next_img(NextModeNumber);

        return p;
    }

    void Next_img(int Now_Block) // 다음 블럭의 이미지를 보이게 합니다.
    {
        if (Now_Block + 1 >= NextMode.Length) Now_Block = 0;
        else Now_Block = Now_Block + 1;

        for (int i = 0; i < NextBlockPre.Length; i++) // 모든 프리펩 비활성화
        {
            NextBlockPre[i].SetActive(false);
        }

        NextBlockPre[NextMode[Now_Block] - 1].SetActive(true);
    }

    void NextBlock() // 다음 블럭을 세팅하겠습니다.
    {
        if (GameOver) return;

        Down_Max = false;
        count = 0;

        mode = Next_Mode_Set();
        int[,] NextBlock = NextOrgBlock(mode);

        for (int i = 0; i < 4; i++)
        {
            if (StartCheck)
            {
                Map[y[i], x[i]] = 1; // 값 세팅

            }

            y[i] = NextBlock[i, 0];
            x[i] = NextBlock[i, 1];

            if (StartArrCheck(y[i], x[i]))
            {
                GameEnd();
                break;
            }


            Map[y[i], x[i]] = 1; // 바뀐값 위치 다시 세팅
        }



        ArrCheck();
    }

    bool StartArrCheck(int y, int x) // 게임 종료 조건이 맞는지 체크합니다.
    {
        if (Map[y, x] != 0) GameOver = true;
        else GameOver = false;

        return GameOver;
    }

    public void GameEnd() // 게임 종료시
    {
        StopAllCoroutines();
        Combo.GameStop();
        BackGround_Music.Stop();
        Score_Ani.GameStop();

        UI.ScoreBorad.SetActive(true);
    }





    // 이동할려는 방향에 값이 있는지 체크하는 함수

    public void ArrSet(int Value) // Value 값으로 현재 위치의 값을 세팅하겠습니다.
    {
        for (int i = 0; i < 4; i++) Map[y[i], x[i]] = Value;
    }

    public bool MapEmpty(int Dir) // 자리가 특정 방향으로 비어있는지 체크합니다.
    {
        bool p = true;

        for (int i = 0; i < 4; i++)
        {
            if (logic.Dir_Check(Map, y[i], x[i], Dir, 0)) p = true;
            else { return false; }
        }

        return p;
    }

    public void ArrAdd(int add_y, int add_x) // 현재 위치값을 일정 수치 만큼 더 합니다.
    {
        for (int i = 0; i < 4; i++)
        {
            y[i] += add_y;
            x[i] += add_x;

            if (y[i] == Map.GetLength(0)) Down_Max = true;
        }

        if (Down_Max) NextBlock();
    }





    // 제거해야할 줄이 있는지 체크하는 함수

    void ArrCheck() // 한줄이 다 찾는지 체크합니다.
    {

        for (int i = 0; i < Map.GetLength(0); i++)
        {
            int count = 0;

            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j] == 1) count++;
            }

            if (count >= Map.GetLength(1)) // 점수를 추가해야할 때
            {
                AddScore();

                for (int j = 0; j < Map.GetLength(1); j++) Map[i, j] = 0;
                ArrDown(i - 1);
            }
        }

        if (!GameOver) tetris_Map.Block_Reset();

    }


    void AddScore() // 점수 추가할 때
    {
        UI.Add_Score(300);
        Score_Ani.Add_Score();
        Score_Add_Sounrd.Play();
        Combo.Time_Add();
    }

    void ArrDown(int hight) // height 위에 있는 값들을 모두 한칸씩 밑으로 내립니다.
    {
        for (int i = hight; i > 2; i--)
        {
            for (int j = Map.GetLength(1) - 1; j > -1; j--)
            {
                int value = Map[i, j];
                Map[i, j] = 0;
                Map[i + 1, j] = value;

                tetris_Map.BlockColorDown(i,j);
            }
        }
    }



    // 모양을 변경하는 함수

    public void ChangeForm() // z키를 누를시 블럭 모양을 바꿉니다.
    {
        ArrSet(0); // 현재 배열값 초기화
        int[][,] Block = BlockMode(mode);

        count = (count + 1) % Block.GetLength(0);

        int[] _y = new int[4]; // 그 다음 변형 형태의 값
        int[] _x = new int[4];
        bool EmptyCheck = true;

        for (int i = 0; i < 4; i++)
        {
            _y[i] = y[i];
            _x[i] = x[i];
        }


        for (int i = 0; i < 4; i++)
        {
            _y[i] += Block[count][i, 0];
            _x[i] += Block[count][i, 1];

            if (_y[i] < 0 || _y[i] == Map.GetLength(0) - 1 || _x[i] < 0 || _x[i] > Map.GetLength(1) - 1)
            {
                EmptyCheck = false;
                break;
            }

            if (Map[_y[i], _x[i]] != 0) EmptyCheck = false; // 만약 변경한 위치에 다른 값이 존재 한다면 
        }


        if (EmptyCheck) // 만약 모양의 변경이 가능하다면
        {
            y = _y; // 현재 위치값을 변경
            x = _x;
        }


        else if (!EmptyCheck)// 만약 모양의 변경이 불가능하다면 
        {
            count -= 1;
            if (count < 0) count = Block.GetLength(0) - 1;
        }

        ArrSet(1);
        if (!GameOver) tetris_Map.Block_Reset();

    }



    // 방향키를 눌렀을 때 이동을 담당하는 함수

    public void Down() // 현재 위치에서 한칸 밑으로 내리는 함수입니다.
    {
        DownDleay = true;
        StartCoroutine(Move.Down_Delay_On());
        ArrSet(0); // 현재 위치 초기화 

        if (MapEmpty((int)Logic2D.Direction.D)) // 이동할 칸이 비어있다면
        {
            ArrAdd(1, 0); // 배열 위치값을 옮기고
            ArrSet(1); // 현재 배열 위치안에 1을 다시 넣는다
        }

        else NextBlock();// 밑에 칸에 다른 블럭이 존재 한다면 다음 블럭 실행

        if (!GameOver) tetris_Map.Block_Reset();
    }


    public void Max_Down()
    {
        DownDleay = true;
        StartCoroutine(Move.Down_Delay_On());
        ArrSet(0); // 현재 위치 초기화 

        for(int i=0;i<Map.GetLength(0);i++)
        {
            if (MapEmpty((int)Logic2D.Direction.D)) ArrAdd(1, 0);
        }

        ArrSet(1);
        if (!GameOver) tetris_Map.Block_Reset();

        NextBlock();      
    }






    public void RL_Move(int add_y, int add_x, int Value) // 오른쪽 왼쪽을 움직이는 함수입니다.
    {
        ArrAdd(add_y, add_x);
        ArrSet(Value);
    }




}
