using UnityEngine;

public class Tetris_Map : MonoBehaviour
{
    public GameObject Block;
    Tetris_GameManager GameManager;
    public Sprite[] Block_img;

    GameObject[,] Block_Loc;

    void Awake()
    {
        GameManager = GetComponent<Tetris_GameManager>();
        Block_Loc = new GameObject[GameManager.Map.GetLength(0) , GameManager.Map.GetLength(1)];

        Block_Setting();
    }



    void Block_Setting() // 게임 시작시 블럭을 세팅
    {
           
        for (int height = 0 ; height < GameManager.Map.GetLength(0) ; height++)
        {
            float x = Block.transform.position.x;
            float y = Block.transform.position.y + (height * -0.5f);

            for (int width = 0; width < GameManager.Map.GetLength(1); width++)
            {
                Block_Loc[height, width] = Instantiate(Block , new Vector3(x,y,0),Quaternion.identity);
                Block_Loc[height, width].SetActive(false);

                x += 0.5f;
            }
        }

    }

    public void Block_Reset() // 배열 안에 들어있는 값에 맞춰 보이는 이미지를 리셋
    {
        
        for (int height = 0; height < GameManager.Map.GetLength(0); height++)
        {
            for (int width = 0; width < GameManager.Map.GetLength(1); width++)
            {
                if (GameManager.Map[height, width] != 0)
                {
                    Block_Loc[height, width].SetActive(true);
                    BlockColor(height, width);
                }


                else Block_Loc[height, width].SetActive(false);
            }
                         
        }

       

    }


    void BlockColor(int height , int width) // 현재 위치의 블럭 색깔을 바꿉니다.
    {
        for(int i=0;i<4;i++)
        {
            if (GameManager.y[i] == height && GameManager.x[i] == width)
            {
                Block_Loc[height, width].GetComponent<SpriteRenderer>().sprite = Block_img[GameManager.mode - 1];
            }
        }

    }

    public void BlockColorDown(int y , int x) // 제거된 줄 위의 색깔을 불러옵니다.
    {
        Sprite c = Block_Loc[y, x].GetComponent<SpriteRenderer>().sprite;
        Block_Loc[y + 1, x].GetComponent<SpriteRenderer>().sprite = c;
    }
   

    


}
