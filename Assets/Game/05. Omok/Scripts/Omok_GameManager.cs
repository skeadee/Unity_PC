using UnityEngine;
using LogicBase;
using UnityEngine.UI;

public class Omok_GameManager : MonoBehaviour
{
    int[] Dir = new int[] { 0,1,2,3,4,5,6,7 };
    public int[] SameLength;

    public int[,] Map = new int[19, 19];
    public int turn = 0;
    public bool Win = false;
    GameObject turn_txt;
    Text txt;

    Logic2D logic;
    Omok_Mouse Mouse;
    AudioSource Audio;

    void Awake()
    {
        logic = new Logic2D();

        for (int i=0;i<19;i++)
        {
            for(int j=0;j<19; j++)
            {
                Map[i, j] = 0;
            }
        }
    }

    void Start()
    { 
        SameLength = new int[8];
        Mouse = GetComponent<Omok_Mouse>();
        turn_txt = GameObject.Find("GameEndtxt");
        txt = turn_txt.GetComponent<Text>();
        Audio = GetComponent<AudioSource>();

        turn_txt.SetActive(false);
    }

    public void Setvalue(int y, int x)
    {
        Map[y, x] = turn + 1;
        
       
        logic.Value_Set(Map, Mouse.Arr_y, Mouse.Arr_x, Dir, (turn + 1));  // 1. 같은 값이 몇개 있는지 확인한다

        if(logic.Same_Length(ref SameLength)) Win_Check();

        
        turn = (turn == 0) ? 1 : 0;
    }

    void Win_Check() // 2. 안에 들어있는 값이 4개 이상이면 게임 종료 
    {
         for (int i = 0; i < SameLength.Length; i++) { if (SameLength[i] >= 4) Win = true; }

        if (Win) Win_Text();
    }

    void Win_Text() // 3. turn에 맞춰서 text 출력 
    {
        Audio.Play();

        if (turn == 0)
        {
            txt.color = Color.black;
            txt.text = "Block Win!!";
        }
            
        else
        {
            txt.color = Color.white;
            txt.text = "White Win!!";
        }

        turn_txt.SetActive(true);
    }
 

}
