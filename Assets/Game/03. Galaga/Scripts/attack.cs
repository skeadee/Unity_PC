using System.Collections;
using UnityEngine;

public class attack : MonoBehaviour
{
   [Tooltip("공격 스피드")] public float speed;
    Vector3 pos;

    GallagGameManager gallag;

    void Awake()
    {
        gallag = GameObject.Find("GameManager").GetComponent<GallagGameManager>();
    }



    void Start()
    {
        pos = new Vector3(0, speed * 0.1f);
    }

    void FixedUpdate()
    {
        if (GallagGameManager.GameMode == 2 || GallagGameManager.GameMode == 4) return;

        gameObject.transform.position += pos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "EnemyAttack(Clone)") return;


        if(col.gameObject.name != "BackGround" && col.gameObject.name != "player") // 충돌한 오브젝트가 player가 아니라면 
        {
            Destroy(this.gameObject);   // 총알을 삭제 한다
        }
      
       

        if (col.gameObject.tag == "enemy")  // 적 태그와 충돌시 점수 추가 및 오브젝트를 삭제 한다
        {
            gallag.score += 300;
            col.gameObject.SetActive(false);
        }
    }


    private void OnBecameInvisible() // 이 오브젝트가 카메라 밖으로 나갔을때 호출한다 
    {
        Destroy(gameObject);
    }
}
