using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject[] Plank;
    Vector2 loc = new Vector2(0, -1.8f);
    float Add = 0;
    public GameObject[] floors;
    public GameObject enemy;

    bool check = true;

    AngryGameMananger angry;


    private void Start()
    {
        angry = GetComponent<AngryGameMananger>();
    }


    void Update()
    {
        if (angry.GameMode == 0 && check)
        {
            Building_Porcess();
            check = false;
        }

        if(angry.GameMode == 3 && !check)
        {
            check = true;
        }
    }

    public void Building_Porcess()
    {
        loc = new Vector2(0, -1.8f);
        Add = 0;

        Transform set = GameObject.Find("Floors").GetComponent<Transform>();
        set.position = new Vector3(0, 0, 0);

        int first = Floor(3, 10);
        Floor_Set(first, 0);

        int second = Floor(2, first);
        Floor_Set(second, 1);
        Locition_Set(first - second, 1);

        int third = Floor(1, second);
        Floor_Set(third, 2);
        Locition_Set(second - third, 2);


       
        set.position = new Vector2(25, 0);   // 생성된 건물 전체를 옆으로 옮기는 코드
    }



    void Floor_Set(int floor, int num)
    {
        int enemy_set = Random.Range(0, floor);

        for (int i = 0; i < floor; i++)
        {
            if (i == 0)
            {
                GameObject plank;

                if (i == enemy_set)
                {
                    plank = Instantiate(Plank[2], loc, Quaternion.identity, floors[num].transform);
                }


                else
                {
                    plank = Instantiate(Plank[0], loc, Quaternion.identity, floors[num].transform);
                }


                loc.x += 3;


            }

            else
            {
                GameObject plank;

                if (i == enemy_set)
                {
                     plank = Instantiate(Plank[3], loc, Quaternion.identity, floors[num].transform);
                }

                else
                {
                    plank = Instantiate(Plank[1], loc, Quaternion.identity, floors[num].transform);
                }

                loc.x += 1.7f;

            }

        }

        loc.x = 0;
        loc.y += 2.1f;
    }

    void Locition_Set(int count, int num)
    {
        Add += count * 0.8f;
        floors[num].gameObject.transform.position += new Vector3(Add, 0, 0);
    }

    int Floor(int min, int max)
    {
        int floor = Random.Range(min, max);
        return floor;
    }


}
