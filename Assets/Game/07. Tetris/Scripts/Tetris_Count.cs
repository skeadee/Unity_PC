using UnityEngine;

public class Tetris_Count : MonoBehaviour
{
    Tetris_GameManager GameManager;
    Tetris_Move Move;
    Tetris_UI UI;

    void Start()
    {
        GameObject obj = GameObject.Find("GameManager");

        GameManager = obj.GetComponent<Tetris_GameManager>();
        Move = obj.GetComponent<Tetris_Move>();
        UI = obj.GetComponent<Tetris_UI>();
    }


    void OnDestroy()
    {
        GameManager.GameStart();
        Move.GameStart();
        UI.GameStart();
    }
}
