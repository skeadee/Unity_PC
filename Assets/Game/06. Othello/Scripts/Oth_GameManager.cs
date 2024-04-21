using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogicBase;
using UnityEngine.UI;

public class Oth_GameManager : MonoBehaviour
{
    public int[,] Map = new int[8, 8];
    public int turn = 1;
    int[] Dir = new int[] {0,1,2,3,4,5,6,7};

    int[] SameLength;
    bool[] DifferentValue;
    int[,] SameLoc;
    List<int> ChangeDir;

    public bool GameEnd = false;
    public int Block, White;

    public Text block_text, white_text;
    public Text GameEnd_text;


    Logic2D logic;

    void Awake()
    {
        logic = new Logic2D();

        Map[3, 3] = 2;
        Map[3, 4] = 1;
        Map[4, 3] = 1;
        Map[4, 4] = 2;
    }

    void Start()
    {
        GameEnd_text.gameObject.SetActive(false);
    }

    

    public void Next_Turn()
    {
        turn = (turn == 1) ? 2 : 1;
    }

   
    public bool DirCheck(int y , int x) // 1. �ٲ� ���� �ִ��� üũ�Ѵ� (���콺 )
    {
        int CheckValue = (turn == 1) ? 2 : 1;
        bool isValueChang = false;
        ChangeDir = new List<int>();

        logic.Value_Set(Map, y, x, Dir, CheckValue);

        logic.Same_Length(ref SameLength); 
        logic.Different_Value(ref DifferentValue); 
     
        if (SameLength.Length == 0 || DifferentValue.Length == 0) return isValueChang; 

        for(int i=0;i< Dir.Length; i++)
        {
            if (SameLength[i] != 0 && DifferentValue[i]) // ���� �����⿡ �ٸ� ���� �ִ°�? , ���� ���⿡ ���� ���� �ִ°�?
            {
                isValueChang = true;
                ChangeDir.Add(i); // ���� ����
            }

              
        }

        return isValueChang;
    }

    public void ChangeValue(int y , int x) // 2. ���콺 Ŭ���� �迭 ���� �����Ѵ�
    {
        Map[y, x] = turn;

        int CheckValue = (turn == 1) ? 2 : 1;
        int[] Dir = new int[ChangeDir.Count]; 

        for (int i = 0; i < ChangeDir.Count; i++) Dir[i] = ChangeDir[i]; // ������ ������ �迭�� �ٽ� �����Ѵ�
       
        logic.Value_Set(Map, y, x, Dir, CheckValue); // ���Ӱ� ������ ���⿡���� Ž���� �����Ѵ�
        logic.Same_Location(ref SameLoc);

        for(int i=0;i< SameLoc.GetLength(0);i++)
        {
            Map[SameLoc[i, 0], SameLoc[i, 1]] = turn;        
        }

    }


    public void GameEndCheck()
    {
        int check = 0;

        Block = 0;
        White = 0;


        for (int i =0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
                if (Map[i, j] != 0) check++;

                if (Map[i, j] == 1) Block++;
                if (Map[i, j] == 2) White++;
            }
        }

        if (check == Map.Length)
        {
            GameEnd = true;

            if (Block > White) { GameEnd_text.text = "Block Win!!"; GameEnd_text.color = Color.black; }
            else if (Block < White) GameEnd_text.text = "Whtie Win!!";
            else if(Block == White) GameEnd_text.text = "Draw";

            GameEnd_text.gameObject.SetActive(true);
            GameEnd_text.gameObject.GetComponent<AudioSource>().Play();
        }

        block_text.text = " Block : " + Block.ToString("00");
        white_text.text = " White : " + White.ToString("00");

       
    }
   



}
