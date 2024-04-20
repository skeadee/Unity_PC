using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagAttack : Beziermove
{
    public Vector3[] AttackRoute;
    public Vector3[] BackRoute;
    public float speed = 2.5f;
    public bool attackOn = false;
    
    
    GallagEnemyMove move;

    bool playerCheck = false;
    bool backCheck = false;

     void Start()
     {
        move = gameObject.GetComponent<GallagEnemyMove>();
     }

    void Update()
    {
        if(GallagGameManager.GameMode == 4)
        {
            StopAllCoroutines();
            return;
        }


        if (backCheck) Route[Route.Length - 1] = move.loc; // 마지막 돌아갈 위치를 받는다
        if (BackRoute.Length == 0 && Route.Length != 0) Route[Route.Length - 1] = move.loc; // 돌아 가는 경로가 없을 때 현재 루트의 마지막 부분 강제 변경
    }

    void StartAttack()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        move.repect = false;


        yield return StartCoroutine(player());
        if (BackRoute.Length != 0) yield return StartCoroutine(back());

        move.repect = true;
    }


    IEnumerator player()
    {
        Route = AttackRoute;
        playerCheck = true;
        Route[0] = move.loc;


        if(gameObject.GetComponent<bulletCall>() == true && attackOn)
        {
            gameObject.GetComponent<bulletCall>().CreateBullet();
        }
        
        
        yield return StartCoroutine(bezierMove(gameObject, 100, speed));
        
        playerCheck = false;
    }

    IEnumerator back()
    {
        Route = BackRoute;
        backCheck = true;
        yield return StartCoroutine(bezierMove(gameObject, 100, speed));
      
        backCheck = false;
    }    

  


   

}
