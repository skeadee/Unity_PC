using UnityEngine;
using UnityEngine.UI;

public class MainGames : MonoBehaviour
{
    int buttonNumber;
    public GameObject[] Games;
    public GameObject[] videos;

    public string[] bestNames; // �� ������ �����ص� �ְ� ���� �̸� , ���� ������ �°� �ֱ�
    public string[] bestScore; // �� ������ �����ص� �ְ� ����  

    public string[] GamesName; // ���� �̸�
    Text gameNametxt;
    Text best_txt;

    public GameObject Mark; // �հ���

    RectTransform pos;
    int markCheck; // ���� �հ��� ��ũ�� �ִ� ��ġ

    public GameObject playButton;

    void Awake()
    {
        gameNametxt = GameObject.Find("GameName").GetComponent<Text>();
        best_txt = GameObject.Find("BestScoretxt").GetComponent<Text>();
    }

    void Start()
    { 
        buttonNumber = 0;
        markCheck = 0;
        pos = Mark.GetComponent<RectTransform>();

        Instantiate(Mark, Games[0].transform); // �հ��� ��ġ ù��° ����
        videos[0].SetActive(true); // ù��° ���� Ȱ��ȭ �ϱ�
        best_text_Set(); // ù��° ���� �ְ� �÷��̾� �ؽ�Ʈ ���
    }

    public void mouseOn(int Num) // ���콺�� ���� �����ܿ� ����� ��
    {
        buttonNumber = Num; // ��ư ��ġ�� �����Ѵ�
    }

    void Update()
    {
        keyBorad();

        if (buttonNumber != markCheck)
        {
            reset(); // �հ��� ��ġ�� �ٲٱ� ���� �����ؾ� �� �͵�
            markCheck = buttonNumber; // ���Ӱ� �հ��� ��ġ�� �����Ѵ�

            markSet();
            textSet();
            videoSet();
            best_text_Set();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            playButton.GetComponent<ButtonImgChange>().click_In();
        }

        if(Input.GetKeyUp(KeyCode.Return))
        {
            playButton.GetComponent<ButtonImgChange>().click_Out();
            NextScene();
        }

    }

    void keyBorad()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && buttonNumber + 1 < Games.Length)
        {
            buttonNumber += 1;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && buttonNumber - 1 > -1)
        {
            buttonNumber += -1;
        }



    }

    void reset() // ���־��ϴ� �� 
    {
        GameObject mar = Games[markCheck].transform.Find("mark(Clone)").gameObject;
        Destroy(mar); // ���� �հ����� �����

        videos[markCheck].SetActive(false); // ���� ��ġ�� ������ ��Ȱ��ȭ �ϱ�
    }


    void markSet()
    {
        Instantiate(Mark, Games[markCheck].transform); // ���ο� ��ġ�� �հ����� �հ��� ��ġ�� �ű��   
    }

    void textSet()
    {
        gameNametxt.text = GamesName[markCheck];
    }

    void videoSet()
    {
        videos[markCheck].SetActive(true);
    }

    void best_text_Set()
    {
        string name = PlayerPrefs.GetString(bestNames[markCheck] , "None");
        int score = PlayerPrefs.GetInt(bestScore[markCheck] , 0);

        best_txt.text = name + "(" + score + ")";
    }

    public void NextScene()
    { 
        GameObject.Find("GameManager").GetComponent<LoadScene>().loadScene(markCheck + 3);
    }

    



}
