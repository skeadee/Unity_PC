using UnityEngine;

public class N_EnemyLoacation : MonoBehaviour
{
    N_GameManager GameManager;

    public GameObject[] enemyPos; // ���� ��ȯ�� ��ġ
    public GameObject[] enemy; // �� ������

    public float interval = 1f; // ���� ��ȯ�ϴ� ����
    float i; // ��ȯ�ϴ� ���� ���� ����

    public int SpeedUP;

    void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
    }

    void Start()
    { 
        i = interval;

        SpeedUP = (int)GameManager.Timer / 2;
    }

    void Update()
    {
        if (GameManager.GameStop) return;

        i -= Time.deltaTime;

        if (SpeedUP > GameManager.Timer && SpeedUP > 10)
        {
            SpeedUP /= 2;
            interval /= 2;
        }


        if (i < 0) enemySet();
      
    }

    void enemySet()
    {
        int e = Random.Range(0, enemy.Length); // �� ���� ����
        int pos = Random.Range(0, enemyPos.Length); // �� ��ȯ�� ��ġ ����

        Instantiate(enemy[e], enemyPos[pos].transform.position , Quaternion.identity);
        i = interval;
    }

}
