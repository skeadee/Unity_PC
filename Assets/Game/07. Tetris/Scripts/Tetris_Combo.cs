using UnityEngine;
using UnityEngine.UI;

public class Tetris_Combo : MonoBehaviour
{
   
    Slider Combo_Time;
    Animator ani;

    int Combo;
    bool GameStop_Check = false;

    public Text Combo_txt;

    public float remainingTime = 5f;

    void Start()
    {
        Combo_Time = GetComponent<Slider>();
        ani = GameObject.Find("Combo_txt").GetComponent<Animator>();

        Combo_Time.maxValue = remainingTime;
        Combo_Time.value = 0;
        Combo = 0;

        Combo_txt.gameObject.SetActive(false);
    }

    public void Time_Add()
    {
        Combo_txt.gameObject.SetActive(true);

        Combo_Time.value = remainingTime;

        Combo++;
        Combo_txt.text = "Combo " + Combo.ToString();  
        ani.SetTrigger("Combo");
    }

    void FixedUpdate()
    {
        if (GameStop_Check) return;

        Combo_Time.value -= Time.deltaTime;

        if (Combo_Time.value == 0 && Combo != 0)
        {
            Combo = 0;
            Combo_txt.text = "Combo " + Combo.ToString();
            Combo_txt.gameObject.SetActive(false);
        }

    }

    public void GameStop()
    {
        GameStop_Check = true;
    }

    public void GameStart()
    {
        GameStop_Check = false;
    }


}
