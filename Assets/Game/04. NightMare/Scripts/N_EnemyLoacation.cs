using UnityEngine;

public class N_EnemyLoacation : MonoBehaviour
{
    N_GameManager GameManager;

    public GameObject[] enemyPos; // 적이 소환될 위치
    public GameObject[] enemy; // 적 프리펩

    public float interval = 1f; // 적을 소환하는 간격
    float i; // 소환하는 간격 받을 변수

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
        int e = Random.Range(0, enemy.Length); // 적 유형 고르기
        int pos = Random.Range(0, enemyPos.Length); // 적 소환될 위치 고르기

        Instantiate(enemy[e], enemyPos[pos].transform.position , Quaternion.identity);
        i = interval;
    }

}
