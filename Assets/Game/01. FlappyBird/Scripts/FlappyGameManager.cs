using UnityEngine;
using UnityEngine.UI;

public class FlappyGameManager : MonoBehaviour
{
    Bird bird;
    Column column;
    AudioSource Audio;
    flappyOptions Option;

    int score;
    bool GameEndCheck = false;
    public Text score_txt;
    public Text name_txt;

    public int Life;
    public GameObject[] Life_img;
    public GameObject[] Grounds;
    public GameObject Player;

    public GameObject scoreboard;
    public bool GameStart_Check = false;

    void Start()
    {
        bird = GameObject.Find("Bird").GetComponent<Bird>();
        Option = GameObject.Find("Pause").GetComponent<flappyOptions>();
        column = GetComponent<Column>();
        Audio = GetComponent<AudioSource>();

        score = 0;
        Life = 3;

        name_txt.text = "Name : " + PlayerPrefs.GetString("NowName" , "null");    
    }


    public void GameStart()
    {
        GameStart_Check = true;

        for (int i = 0 ; i < Grounds.Length ; i++) Grounds[i].GetComponent<HorzScroll>().Move();
        for (int i = 0 ; i < column.obj.Length ; i++) column.obj[i].GetComponent<HorzScroll>().Move();

        Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Player.GetComponent<Animator>().enabled = true;

        column.GameStart();
    }


    void Life_Img()
    {
        if (Life < 1) for (int i = 0; i < Life_img.Length; i++) Destroy(Life_img[i]);
        else Destroy(Life_img[Life]);
    }

    public void Hit(int damgae) // 플레이어가 데미지를 입을 때
    {
        Life -= damgae;

        Life_Img();
        if (Life < 1) GameEnd();     
    }

    public void ScoreAdd(int add_Score)
    {
        score += add_Score;

        score_txt.text = "score : " + score;
    }


    void GameEnd() // 게임 끝
    {
        if (GameEndCheck) return;

        GameEndCheck = true;
        for (int i = 0; i < Option.horzScroll.Count; i++) Option.horzScroll[i].Stop();

        bird.Die();
        column.GameStop();
        Audio.Play();

        PlayerPrefs.SetInt("flappyscore", score);
        scoreboard.SetActive(true);
    }


}
