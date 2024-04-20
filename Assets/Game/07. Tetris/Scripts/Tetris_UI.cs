using UnityEngine;
using UnityEngine.UI;

public class Tetris_UI : MonoBehaviour
{
    Tetris_GameManager GameManager;
    Tetris_Move Move;
    Tetris_Combo Combo;
    Tetris_ScoreAni ScoreAni;

    public GameObject Option_Panel;
    public float Level_Time;
    float time;
    public Text Next_Nevel_txt;

    public AudioSource background;
    bool background_check = true;
    public bool StopGame = false;
    public GameObject ScoreBorad;

    public int Score;
    public Text Score_txt;

    count Count;

    public Text Now_Level_txt;
    int Now_Level = 2;

    void Start()
    {
        GameManager = GetComponent<Tetris_GameManager>();
        Move = GetComponent<Tetris_Move>();
        Count = GameObject.Find("count").GetComponent<count>();
        Combo = GameObject.Find("Combo_Time").GetComponent<Tetris_Combo>();
        ScoreAni = GetComponent<Tetris_ScoreAni>();


        time = Level_Time;
        

        Score = 0;
    }

    bool StartCheck = false;

    public void GameStart()
    {
        StartCheck = true;
    }

    void FixedUpdate()
    {
        if (StopGame || GameManager.GameOver || !StartCheck) return;


        if(time < 0.5f)
        {
            time = Level_Time;
            Next_Level();
        }

        time -= Time.deltaTime;     

       
    }

    void Next_Level()
    {
        Next_Nevel_txt.text = "Level UP!";
        Next_Nevel_txt.gameObject.SetActive(true);

        Now_Level_txt.text = "Level " + Now_Level.ToString();
        Now_Level++;

        Move.NextLevel();
    }


    public void Add_Score(int Value)
    {
        Score += Value;
        Score_txt.text = Score.ToString();
        PlayerPrefs.SetInt("TetrisScore", Score);
    }


  

    public void BackGournd_Music(Button button)
    {
        ColorBlock colorbutton = button.colors;

        if (!background_check)
        {      
            colorbutton.normalColor = new Color32(255, 255, 255, 255);
            button.colors = colorbutton;
        }

        else if(background_check)
        { 
            colorbutton.normalColor = new Color32(255, 255, 255, 128);
            button.colors = colorbutton;          
        }

        background_check = !background_check;
    }


    public void Option()
    {
        if (GameManager.GameOver) return;


        if (Option_Panel.activeSelf) // 옵션창 비활성화
        {
            StopGame = false;
            Option_Panel.SetActive(false);
            if (background_check) background.Play();
            if (GameManager.StartCheck) Move.GameStart();
            Combo.GameStart();
            ScoreAni.GameStart();

            if (Count != null) Count.CountStart();
        }

        else // 옵션창 활성화
        {
            StopGame = true;
            Option_Panel.SetActive(true);
            background.Pause();
            Combo.GameStop();
            ScoreAni.GameStop();
            Move.GameStop();

            if (Count != null) Count.CountStop();

        }
    }


}
