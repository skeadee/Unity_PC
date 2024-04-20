using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform _player;
    NavMeshAgent _nav;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _nav = GetComponent<NavMeshAgent>();

    }


    void Update()
    {
        _nav.SetDestination(_player.position);   
    }
}
