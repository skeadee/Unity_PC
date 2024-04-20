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
    public int count = 0; // �� ü���� ���� üũ �ϴ� ����

    public bool Down_Max;
    int[] NextMode = new int[3];
    int NextModeNumber = -1;
    public bool GameOver = false;

    public int[] y; // ���� ��ġ ��
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

        NextBlock(); // ���۽� 
        tetris_Map.Block_Reset();
        StartCheck = true;    
    }


    // ���� ���� �����ϴ� �Լ�

    int Next_Mode_Set() // ���� ���� ������ �������� �����մϴ�.
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

    void Next_img(int Now_Block) // ���� ���� �̹����� ���̰� �մϴ�.
    {
        if (Now_Block + 1 >= NextMode.Length) Now_Block = 0;
        else Now_Block = Now_Block + 1;

        for (int i = 0; i < NextBlockPre.Length; i++) // ��� ������ ��Ȱ��ȭ
        {
            NextBlockPre[i].SetActive(false);
        }

        NextBlockPre[NextMode[Now_Block] - 1].SetActive(true);
    }

    void NextBlock() // ���� ���� �����ϰڽ��ϴ�.
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
                Map[y[i], x[i]] = 1; // �� ����

            }

            y[i] = NextBlock[i, 0];
            x[i] = NextBlock[i, 1];

            if (StartArrCheck(y[i], x[i]))
            {
                GameEnd();
                break;
            }


            Map[y[i], x[i]] = 1; // �ٲﰪ ��ġ �ٽ� ����
        }



        ArrCheck();
    }

    bool StartArrCheck(int y, int x) // ���� ���� ������ �´��� üũ�մϴ�.
    {
        if (Map[y, x] != 0) GameOver = true;
        else GameOver = false;

        return GameOver;
    }

    public void GameEnd() // ���� �����
    {
        StopAllCoroutines();
        Combo.GameStop();
        BackGround_Music.Stop();
        Score_Ani.GameStop();

        UI.ScoreBorad.SetActive(true);
    }





    // �̵��ҷ��� ���⿡ ���� �ִ��� üũ�ϴ� �Լ�

    public void ArrSet(int Value) // Value ������ ���� ��ġ�� ���� �����ϰڽ��ϴ�.
    {
        for (int i = 0; i < 4; i++) Map[y[i], x[i]] = Value;
    }

    public bool MapEmpty(int Dir) // �ڸ��� Ư�� �������� ����ִ��� üũ�մϴ�.
    {
        bool p = true;

        for (int i = 0; i < 4; i++)
        {
            if (logic.Dir_Check(Map, y[i], x[i], Dir, 0)) p = true;
            else { return false; }
        }

        return p;
    }

    public void ArrAdd(int add_y, int add_x) // ���� ��ġ���� ���� ��ġ ��ŭ �� �մϴ�.
    {
        for (int i = 0; i < 4; i++)
        {
            y[i] += add_y;
            x[i] += add_x;

            if (y[i] == Map.GetLength(0)) Down_Max = true;
        }

        if (Down_Max) NextBlock();
    }





    // �����ؾ��� ���� �ִ��� üũ�ϴ� �Լ�

    void ArrCheck() // ������ �� ã���� üũ�մϴ�.
    {

        for (int i = 0; i < Map.GetLength(0); i++)
        {
            int count = 0;

            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j] == 1) count++;
            }

            if (count >= Map.GetLength(1)) // ������ �߰��ؾ��� ��
            {
                AddScore();

                for (int j = 0; j < Map.GetLength(1); j++) Map[i, j] = 0;
                ArrDown(i - 1);
            }
        }

        if (!GameOver) tetris_Map.Block_Reset();

    }


    void AddScore() // ���� �߰��� ��
    {
        UI.Add_Score(300);
        Score_Ani.Add_Score();
        Score_Add_Sounrd.Play();
        Combo.Time_Add();
    }

    void ArrDown(int hight) // height ���� �ִ� ������ ��� ��ĭ�� ������ �����ϴ�.
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



    // ����� �����ϴ� �Լ�

    public void ChangeForm() // zŰ�� ������ �� ����� �ٲߴϴ�.
    {
        ArrSet(0); // ���� �迭�� �ʱ�ȭ
        int[][,] Block = BlockMode(mode);

        count = (count + 1) % Block.GetLength(0);

        int[] _y = new int[4]; // �� ���� ���� ������ ��
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

            if (Map[_y[i], _x[i]] != 0) EmptyCheck = false; // ���� ������ ��ġ�� �ٸ� ���� ���� �Ѵٸ� 
        }


        if (EmptyCheck) // ���� ����� ������ �����ϴٸ�
        {
            y = _y; // ���� ��ġ���� ����
            x = _x;
        }


        else if (!EmptyCheck)// ���� ����� ������ �Ұ����ϴٸ� 
        {
            count -= 1;
            if (count < 0) count = Block.GetLength(0) - 1;
        }

        ArrSet(1);
        if (!GameOver) tetris_Map.Block_Reset();

    }



    // ����Ű�� ������ �� �̵��� ����ϴ� �Լ�

    public void Down() // ���� ��ġ���� ��ĭ ������ ������ �Լ��Դϴ�.
    {
        DownDleay = true;
        StartCoroutine(Move.Down_Delay_On());
        ArrSet(0); // ���� ��ġ �ʱ�ȭ 

        if (MapEmpty((int)Logic2D.Direction.D)) // �̵��� ĭ�� ����ִٸ�
        {
            ArrAdd(1, 0); // �迭 ��ġ���� �ű��
            ArrSet(1); // ���� �迭 ��ġ�ȿ� 1�� �ٽ� �ִ´�
        }

        else NextBlock();// �ؿ� ĭ�� �ٸ� ���� ���� �Ѵٸ� ���� �� ����

        if (!GameOver) tetris_Map.Block_Reset();
    }


    public void Max_Down()
    {
        DownDleay = true;
        StartCoroutine(Move.Down_Delay_On());
        ArrSet(0); // ���� ��ġ �ʱ�ȭ 

        for(int i=0;i<Map.GetLength(0);i++)
        {
            if (MapEmpty((int)Logic2D.Direction.D)) ArrAdd(1, 0);
        }

        ArrSet(1);
        if (!GameOver) tetris_Map.Block_Reset();

        NextBlock();      
    }






    public void RL_Move(int add_y, int add_x, int Value) // ������ ������ �����̴� �Լ��Դϴ�.
    {
        ArrAdd(add_y, add_x);
        ArrSet(Value);
    }




}
