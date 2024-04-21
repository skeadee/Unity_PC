using UnityEngine;
using UnityEngine.UI;

public class AngryGameMananger : MonoBehaviour
{
    public int Score;
    public GameObject[] Lifes;
    public int Life;
    [HideInInspector] public int BirdCheck;
    public GameObject Ball;
    public GameObject scoreBorad;

    public Text NowName;
    public Text Score_txt;
    public GameObject option;

    Building build;
    AngryCamera cam;

    public int GameMode;

    // GameMode = 0 �� ���� ������ , ���⼭ ī�޶� ���� �� ���� �ٽ��ϱ�
    // GameMode = 1 �� ���� ����(���� ��� �� �ִ�)
    // GameMode = 2 �� ���� �Ͻ�����
    // GameMode = 3 �� ���� ����

    void Awake()
    {
        build = GetComponent<Building>();
        cam = GameObject.Find("Main Camera").GetComponent<AngryCamera>();
    }


    void Start()
    {
        NowName.text = "Name : " + PlayerPrefs.GetString("NowName");

        Life = 3;
        BirdCheck = 3;

        Instantiate(Ball);
    }

    

    public void options()
    {
        if (Life <= 0) return;

        if (!option.activeSelf)
        {
            option.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            option.SetActive(false);
            Time.timeScale = 1;
        }
    }



    public float t = 2f;
    bool check = true;


    void FixedUpdate()
    {
        if (GameMode == 0) t -= Time.deltaTime;
        if (t < 0 && check) { GameMode = 1; check = false; }
    }

    void Update()
    {
        Score_txt.text = "Score : " + Score;

        if (GameMode == 3) Reset();
    }



    void Reset()
    {
        if (Life == 0)
        {
            GameMode = 3;

            PlayerPrefs.SetInt("AngryScore", Score);
            scoreBorad.SetActive(true);
        }

        else
        {
            Tag_Destory();
            Instantiate(Ball);

            BirdCheck = 3;
            t = 2f;
            check = true;

            GameMode = 0;
        }

    }


    void Tag_Destory()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }


   



}
