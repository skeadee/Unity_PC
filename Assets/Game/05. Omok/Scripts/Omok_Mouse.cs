using UnityEngine;
using System.Collections;

public class Omok_Mouse : MonoBehaviour
{
    Vector3 mouse;
    float y, x;
    public GameObject[] Doll;

    Omok_GameManager GameManager;

    public int Arr_y , Arr_x; // 배열 위치값
    int last_y, last_x;
    GameObject LastDoll;

    float[] loc_x = new float[19];
    float[] loc_y = new float[19];

    float Start_y = 2.115f;
    float Start_x = -2.115f;
    float OverNum = 0.2f;
    bool Delay = false;

    void Start()
    {
        GameManager = GetComponent<Omok_GameManager>();

        loc_x = new float[19];
        loc_y = new float[19];

        for (int i = 0; i < 19; i++) // 비교값 세팅
        {
            loc_x[i] = Start_x;
            loc_y[i] = Start_y;

            Start_x += 0.235f;
            Start_y -= 0.235f;
        }

    }

    void Update()
    {
        ArrChange(); 

        if (GameManager.Map[Arr_y, Arr_x] != 0 || !MouseCheck() || GameManager.Win) // 2. 배열칸에 값이 있다면 취소 
        {
            LastLoc_Reset();
            return;
        }
           
         Doll_Look(); 

        if (Input.GetMouseButtonDown(0) && !Delay) Doll_Set();
      

    }

    void ArrChange() // 1. 마우스의 위치를 배열 형태의 변수로 바꾼다
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        y = mouse.z;
        x = mouse.x;

        for (int i = 0; i < 19; i++) 
        {
            if (loc_x[i] < x) Arr_x = i;
            if (loc_y[i] > y) Arr_y = i;
        }

    }

    bool MouseCheck() // 3. 마우스가 Map위에 있는지 확인한다
    {
        
        if (x < loc_x[0] - OverNum || x > loc_x[18] + OverNum || y > loc_y[0] + OverNum || y < loc_y[18] - OverNum)
        {
            LastLoc_Reset();
            return false;
        }

        else return true;
    }


    void Doll_Look() // 4. 돌을 trun에 맞게 보이게 한다
    {
        GameObject _Doll;

        if (GameManager.turn == 0) _Doll = Doll[0];
        else _Doll = Doll[1];


        if (Arr_y != last_y || Arr_x != last_x)
        {
            if (LastDoll != null) Destroy(LastDoll);

            last_y = Arr_y;
            last_x = Arr_x;

            LastDoll = Instantiate(_Doll, new Vector3(loc_x[Arr_x] , 0 , loc_y[Arr_y]), Quaternion.identity);
        }
    }

    void Doll_Set() // 5. 마우스 클릭시 돌을 놓는다
    {
        GameManager.Setvalue(Arr_y , Arr_x);

        GameObject obj = Instantiate(LastDoll, new Vector3(loc_x[Arr_x] , 0 , loc_y[Arr_y]), Quaternion.identity);
        obj.GetComponent<Omok_Doll>().AniOn();

        LastLoc_Reset();

        Delay = true;
        StartCoroutine(DelayCheck());
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(1f);
        Delay = false;
    }


    void LastLoc_Reset()
    {
        Destroy(LastDoll);
        last_x = -1;
        last_y = -1;
    }
    


}
