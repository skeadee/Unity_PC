using UnityEngine;
using System.Collections.Generic;


public class flappyOptions : MonoBehaviour
{
    public GameObject optionPanel;

    FlappyGameManager GameManager;
    Column column;
    Bird bird;

    public List<HorzScroll> horzScroll;

    void Start()
    {
        GameObject Manager = GameObject.Find("GameManager");


        horzScroll = new List<HorzScroll>();
        bird = GameObject.Find("Bird").GetComponent<Bird>();
        GameManager = Manager.GetComponent<FlappyGameManager>();
        column = Manager.GetComponent<Column>();

        for (int i = 0; i < GameManager.Grounds.Length; i++) horzScroll.Add(GameManager.Grounds[i].GetComponent<HorzScroll>());
    }


    public void OptionButton()
    {

        if(optionPanel.activeSelf) // 게임 다시 시작 
        {
            optionPanel.SetActive(false);

            if (!GameManager.GameStart_Check)
            {
                GameObject.Find("count").GetComponent<count>().CountStart();
                return;
            }

            for (int i = 0; i < horzScroll.Count; i++) horzScroll[i].Move();

            column.GameStart();
            bird.GameStart();
        }

        else // 게임 정지
        {
            optionPanel.SetActive(true);

            if (!GameManager.GameStart_Check)
            {
                GameObject.Find("count").GetComponent<count>().CountStop();
                return;
            }

            for (int i = 0; i < horzScroll.Count; i++) horzScroll[i].Stop();
             
            column.GameStop();
            bird.GameStop();            
        }


    }

}
