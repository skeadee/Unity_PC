using System.Collections;
using UnityEngine;

public class attack : MonoBehaviour
{
   [Tooltip("���� ���ǵ�")] public float speed;
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


        if(col.gameObject.name != "BackGround" && col.gameObject.name != "player") // �浹�� ������Ʈ�� player�� �ƴ϶�� 
        {
            Destroy(this.gameObject);   // �Ѿ��� ���� �Ѵ�
        }
      
       

        if (col.gameObject.tag == "enemy")  // �� �±׿� �浹�� ���� �߰� �� ������Ʈ�� ���� �Ѵ�
        {
            gallag.score += 300;
            col.gameObject.SetActive(false);
        }
    }


    private void OnBecameInvisible() // �� ������Ʈ�� ī�޶� ������ �������� ȣ���Ѵ� 
    {
        Destroy(gameObject);
    }
}
