using UnityEngine;

public class N_GameManager : MonoBehaviour
{
    public bool GameEnd = false;
    public bool GameStop = false;
    public int score;
    public bool UICheck = false; // ���콺�� ui�� �ö� �ִ��� üũ�ϴ� ����
    public GameObject count;
    public GameObject Player;
    GameObject PausePanel;

    public bool TimeCheck = false; // ���� �ð��� �������� üũ�ϴ� ����
    public float Timer; // ���� �ð�
    int PauseCheck; // �ɼ�â ���� üũ�ϱ�

    void Awake()
    {
        PausePanel = GameObject.Find("PausePanel");

       
    }

    void Start()
    {
        TimeCheck = false;
        PauseCheck = 0;
        PausePanel.SetActive(false);
        GameStop = true; 
        score = 0; // ���� ���� �ʱ�ȭ
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) // esc��ư �������� �Ͻ� ���� �޴� ����
        {
            Pause();
        }

        if(Timer > 0 && !GameStop) Timer -= Time.deltaTime;
        if (Timer < 0) TimeEnd();
    }

    void TimeEnd() // ���� �ð��� �� �Ǿ� ������ ��������
    {
        GameStop = true; // ������ ���� ��Ų��
        TimeCheck = true;
        GameEnd = true;

        Player.GetComponent<Animator>().enabled = false; // �÷��̾��� �ִϸ��̼��� ���� ��Ų��
    }

    public void scorePlus(int plus)
    {
        score += plus;
    }

    public void Pause() // �Ͻ� ���� ��ư ó���ϴ� �Լ�
    {
        if (GameEnd) return;

        if(PauseCheck % 2 == 0)
        {
            if(count == null) GameStop = true;
            if (count != null) count.GetComponent<count>().CountStop();
            Player.GetComponent<Animator>().enabled = false;

            PausePanel.SetActive(true); // �ɼ�â Ȱ��ȭ       
        }

        else
        {
            if (count == null) GameStop = false;
            if (count != null) count.GetComponent<count>().CountStart();

            Player.GetComponent<Animator>().enabled = true;
          

            PausePanel.SetActive(false); // �ɼ�â ��Ȱ��ȭ
        }

        PauseCheck++;
    }

    public void UI_on()
    {
        UICheck = true;
       
    }

    public void UI_end()
    {
        UICheck = false;
       
    }



}
