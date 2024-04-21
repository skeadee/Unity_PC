using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Vector3 move = new Vector3();
    [Range(1, 5)] public int speed;

    public float AttackDelay = 0.5f;
    float Delay;

    public GameObject bullet;
    Animator ani;

    GallagGameManager gallag;

    void Awake()
    {
        gallag = GameObject.Find("GameManager").GetComponent<GallagGameManager>();
        ani = GetComponent<Animator>();
    }

    void Start()
    {
        check = true;
        Delay = AttackDelay;
    }


    void Update()
    {
        if (GallagGameManager.GameMode == 4 || GallagGameManager.GameMode == 0 || GallagGameManager.GameMode == 2) return;

        Delay -= Time.deltaTime;

       
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        move = new Vector3(h, v, 0);

        if (Input.GetKeyDown("z") && Delay < 0)
        {
            // 여기에 공격구현 하기
            Vector3 pos = gameObject.transform.position;
            pos.y += 1f;
            Instantiate(bullet, pos, Quaternion.identity);
            Delay = AttackDelay;
        }

        move *= 0.1f;
    }

    void FixedUpdate()
    {
        gameObject.transform.position += move;
        move = Vector3.zero;
    }

    bool check = true;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.name == "BackGround") return;

        if(gallag.life >= 0 && check)
        {
           if(gallag.life > 0 && col.gameObject.name != "Bullet(Clone)") Destroy(gallag.life_img[--gallag.life]);

           if(gallag.life != 0 && col.gameObject.name != "Bullet(Clone)")
           {
                ani.SetTrigger("crash");
                StartCoroutine(crash());
           }        
        }

        if (gallag.life <= 0) GallagGameManager.GameMode = 4; // Life가 0이면 게임 종료 모드로 전환하기
    }

    IEnumerator crash()
    {
        check = false;
        yield return new WaitForSeconds(4f);
        check = true;
    }

}
