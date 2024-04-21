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
            _nav.enabled = false; // ������ ���߸� ��� �������� �����
            GetComponent<Animator>().enabled = false; // �ִϸ��̼� ����     
        }

        else if(!GameManger.GameStop && !enemyHealth.DieCheck)
        {
            _nav.enabled = true; // ������ ������µ� �ƴϰ� , ���� ���� ���µ� �ƴ϶�� �ٽ� ����
            GetComponent<Animator>().enabled = true; // �ִϸ��̼� ����       
        }

        if (_nav.enabled == true)  _nav.SetDestination(_player.position);
    }
}
