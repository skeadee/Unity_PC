using UnityEngine;

public class N_GameManager : MonoBehaviour
{
    public bool GameEnd = false;
    public bool GameStop = false;
    public int score;
    public bool UICheck = false; // 마우스가 ui에 올라가 있는지 체크하는 변수
    public GameObject count;
    public GameObject Player;
    GameObject PausePanel;

    public bool TimeCheck = false; // 게임 시간이 끝낫는지 체크하는 변수
    public float Timer; // 게임 시간
    int PauseCheck; // 옵션창 변수 체크하기

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
        score = 0; // 게임 점수 초기화
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) // esc버튼 눌렀을때 일시 정지 메뉴 실행
        {
            Pause();
        }

        if(Timer > 0 && !GameStop) Timer -= Time.deltaTime;
        if (Timer < 0) TimeEnd();
    }

    void TimeEnd() // 제한 시간이 다 되어 게임이 끝낫을때
    {
        GameStop = true; // 게임을 정지 시킨다
        TimeCheck = true;
        GameEnd = true;

        Player.GetComponent<Animator>().enabled = false; // 플레이어의 애니메이션을 정지 시킨다
    }

    public void scorePlus(int plus)
    {
        score += plus;
    }

    public void Pause() // 일시 정지 버튼 처리하는 함수
    {
        if (GameEnd) return;

        if(PauseCheck % 2 == 0)
        {
            if(count == null) GameStop = true;
            if (count != null) count.GetComponent<count>().CountStop();
            Player.GetComponent<Animator>().enabled = false;

            PausePanel.SetActive(true); // 옵션창 활성화       
        }

        else
        {
            if (count == null) GameStop = false;
            if (count != null) count.GetComponent<count>().CountStart();

            Player.GetComponent<Animator>().enabled = true;
          

            PausePanel.SetActive(false); // 옵션창 비활성화
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
