using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagPatternManager : MonoBehaviour
{
    public GameObject[] pas_1; // 여기에 공격 패턴 가지고 있는 오브젝트 넣기
    public GameObject[] pas_2;
    public GameObject[] pas_3;
    public GameObject[] pas_4;


    public float time;

    [HideInInspector]public bool repcetNext = false;

     void Update()
     {
        if (GallagGameManager.GameMode == 4)
        {
            StopAllCoroutines();
            return;
        }
            



        if(repcetNext) // 다음 반복 옵션 실행하기
        {
            BroadcastMessage("RepectParrten2");
            StartCoroutine(process());
            repcetNext = false;
        }


      


     }

    IEnumerator process()
    {
        yield return StartCoroutine(attackPattern(pas_1 , time));
        yield return StartCoroutine(attackPattern(pas_2 , time));
        yield return StartCoroutine(attackPattern(pas_3, time));
        yield return StartCoroutine(attackPattern(pas_4, time));

        StartCoroutine(process());
    }


    IEnumerator attackPattern(GameObject[] targets , float time)
    {
        yield return new WaitForSeconds(time);

        for(int i=0;i< targets.Length;i++)
        {
            targets[i].SendMessage("StartAttack" , SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(0.35f);
        }

        yield return new WaitForSeconds(time);
    }


}
