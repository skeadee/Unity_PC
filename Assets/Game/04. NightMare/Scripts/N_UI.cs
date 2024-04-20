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


        if (playerHealth.DieCheck || GameManager.TimeCheck) // ���� �÷��̰� �׾��ٸ� ������ Ȱ��ȭ
        {
            PlayerPrefs.SetInt("NightMaraScore", GameManager.score);
            scorePanel.SetActive(true);
        }

        else // �÷��̾ ���� �ʾҴٸ� 
        {
            Timer.text = "Time : " + (int)GameManager.Timer; // Ÿ�̸� ��� �۵� 
        }

    }



}
