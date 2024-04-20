using UnityEngine;
using UnityEngine.UI;

public class N_UI : MonoBehaviour
{
    public GameObject scorePanel;
   

    N_PlayerHealth playerHealth;
    N_GameManager GameManager;

    public Text Timer;
    public Text score;

    void Awake()
    {
        scorePanel.SetActive(false);
    }

    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<N_PlayerHealth>();
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
    }

    void Update()
    {
        score.text = "Score : " + GameManager.score;


        if (playerHealth.DieCheck || GameManager.TimeCheck) // 만약 플레이가 죽었다면 점수판 활성화
        {
            PlayerPrefs.SetInt("NightMaraScore", GameManager.score);
            scorePanel.SetActive(true);
        }

        else // 플레이어가 죽지 않았다면 
        {
            Timer.text = "Time : " + (int)GameManager.Timer; // 타이머 계속 작동 
        }

    }



}
