using UnityEngine;
using System.Collections;


public class GallagEnemyMove : Beziermove
{
    public Vector3 loc; // 이거 마지막 위치 , 다른 스크립트에서 계속 이동하게 하기 
    public float waitTime;
    public float speed;
    public bool attackOn = false;

   [HideInInspector]public bool repect = false;
    //GallagLevel level;


    
    GallagPatternManager patternManager;
    public GameObject patternmanager;

    bulletCall bullet;  // 이거 관련 수정하기

    public string LastTarget;

    void Start()
    {
        patternManager = patternmanager.GetComponent<GallagPatternManager>();

        if(gameObject.GetComponent<bulletCall>() != null) bullet = GetComponent<bulletCall>();
    
        StartCoroutine(moveStart());
    }

    public IEnumerator moveStart()
    {
        yield return new WaitForSeconds(waitTime);

        if (gameObject.GetComponent<bulletCall>() ==true && attackOn) bullet.CreateBullet(); // 이거 고치기

        yield return StartCoroutine(bezierMove(gameObject , 100, speed));

        

        if (gameObject.name == LastTarget) patternManager.repcetNext = true;
        repect = true;
    }

    bool stopCheck = true; 

    void FixedUpdate()
    {
        if (GallagGameManager.GameMode == 4 && stopCheck)
        {
            StopAllCoroutines();
            stopCheck = false;
        }

        else if(GallagGameManager.GameMode != 4 && !stopCheck)
        {
            StartCoroutine(moveStart());
            stopCheck = true;
        }

        if(!repect) Route[Route.Length - 1] = loc;
        if (repect) gameObject.transform.position = loc;
    }

   
}



