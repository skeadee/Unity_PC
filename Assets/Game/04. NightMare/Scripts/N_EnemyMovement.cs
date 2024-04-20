using UnityEngine;
using UnityEngine.AI;

public class N_EnemyMovement : MonoBehaviour
{
    Transform _player;
    NavMeshAgent _nav;
    N_EnemyHealth enemyHealth;
    N_GameManager GameManger;

    void Awake()
    {
        GameManger = GameObject.Find("GameManager").GetComponent<N_GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<N_EnemyHealth>();
    }

    void Update()
    {
        if (GameManger.GameStop)
        {
            _nav.enabled = false; // 게임이 멈추면 모든 움직임을 멈춘다
            GetComponent<Animator>().enabled = false; // 애니메이션 정지     
        }

        else if(!GameManger.GameStop && !enemyHealth.DieCheck)
        {
            _nav.enabled = true; // 게임이 멈춘상태도 아니고 , 현재 죽은 상태도 아니라면 다시 실행
            GetComponent<Animator>().enabled = true; // 애니메이션 실행       
        }

        if (_nav.enabled == true)  _nav.SetDestination(_player.position);
    }
}
