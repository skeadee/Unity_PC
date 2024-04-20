using UnityEngine;

public class Tetris_ScoreAni : MonoBehaviour
{
    Animator ani;
    public Animator[] add_score;

 
    public void Add_Score()
    {
        for (int i = 0; i < add_score.Length; i++) add_score[i].SetTrigger("Score Add");
    }

    public void GameStop()
    {
        for (int i = 0; i < add_score.Length; i++) add_score[i].speed = 0;
    }


    public void GameStart()
    {
        for (int i = 0; i < add_score.Length; i++) add_score[i].speed = 1;
    }
}
