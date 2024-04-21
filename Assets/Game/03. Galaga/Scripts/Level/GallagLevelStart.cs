using System.Collections;
using UnityEngine;

public class GallagLevelStart : MonoBehaviour
{
    int max = 0;
    int i = 0;
    bool check = false;
   
    public bool start = false; // ���� ������ �ٷ� ������ ���ΰ�

    [Space(15f)]
    public GallagLevelStart Next; // ���� ������ ������ ���� ������ ������ ���ΰ� 
    public float waitTime = 0;  // ���� ������ �����Ѵٸ� �󸶳� ��� �Ŀ� ������ ���ΰ�

 

    void Start()
    {
        max = gameObject.transform.childCount;

        if(start) StartCoroutine(MoveStart());
    }


    public IEnumerator MoveStart()
    {
        GallagEnemyMove Level_1;

        for (; i < max; i++)
        {
            Level_1 = gameObject.transform.GetChild(i).GetComponent<GallagEnemyMove>();
           

            yield return new WaitForSeconds(0.15f);       
        }


        if(Next != null)
        {
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(Next.MoveStart());
        }

    }


    void Update()
    {
        if (GallagGameManager.GameMode == 0 && !check)
        {
            StopAllCoroutines();
            check = true;
        }

        else if (GallagGameManager.GameMode == 1 && check)
        {
            StartCoroutine(MoveStart());
            check = false;
        }

        DestoyCheck();
    }


    void DestoyCheck()
    {
        bool check = false;


        for (int i = 0; i < max; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                check = false;
                break;
            }

            else check = true;

        }

        if (check) Destroy(gameObject);

    }


}

