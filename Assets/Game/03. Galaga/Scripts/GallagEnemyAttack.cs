using System.Collections;
using UnityEngine;

public class GallagEnemyAttack : MonoBehaviour
{
    public float speed = 2.5f;
    

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Vector3 player = (GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position).normalized * 0.1f;

        while (true)
        {
            gameObject.transform.position += player;
            yield return new WaitForSeconds(0.01f * speed);
        }
    }

    void Update()
    {
        if (GallagGameManager.GameMode == 4) StopAllCoroutines();
    }


    void OnBecameInvisible() // �� ������Ʈ�� ī�޶� ������ �������� ȣ���Ѵ� 
    {
        Destroy(gameObject);
    }


}
