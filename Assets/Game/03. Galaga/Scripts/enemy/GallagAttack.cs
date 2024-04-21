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


        if (backCheck) Route[Route.Length - 1] = move.loc; // ������ ���ư� ��ġ�� �޴´�
        if (BackRoute.Length == 0 && Route.Length != 0) Route[Route.Length - 1] = move.loc; // ���� ���� ��ΰ� ���� �� ���� ��Ʈ�� ������ �κ� ���� ����
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
