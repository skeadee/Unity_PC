using System.Collections;
using UnityEngine;

public class GallagGameManager : MonoBehaviour
{
   [HideInInspector] public int score = 0;
    public int life;
    public bool PauseCheck; // 옵션창에서 다른 씬으로 이동한다면 점수 창을 띄우지 않는다

    public GameObject[] life_img;

    public static int GameMode = 0;

    // 게임 모드가 0 이면 게임 시작 전 
    // 게임 모드가 1이면 게임 시작 하게 만들기
    // 게임 모드가 2 이면 게임 일시 정지 //  일단 이건 나중에 구현을 하도록 하자 
    // 게임 모드 4이면 모든 움직임 종료 및 바로점수창 뜨게 만들기

    [Space(15f)]
    public GameObject count;
    public GameObject scoreborad;
    public int i = 0;

    void Start()
    {
        i = 0;
        GameMode = 0;
        life = 3;
        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        yield return new WaitForSeconds(2f);
        count.SetActive(true);
    }

    void Update()
    {
        if(life == 0)
        {
            PlayerPrefs.SetInt("GallagScore", score);
            GameMode = 4;
        }

        if (GameMode == 4 && !scoreborad.activeSelf && !PauseCheck) scoreborad.SetActive(true);
    }

   
}
