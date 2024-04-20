using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private const float timeBetweenAttacks = 0.5f;
    private const int attackDamage = 2;

    private GameObject player;
    private PlaterHelath playerHP;
    private bool playerInRange = false;
    private float timer;


    void Awake()
    {
        player = GameObject.Find("Player");
        playerHP = player.GetComponent<PlaterHelath>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }

      
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }


    }

    void Attack()
    {
        timer = 0f;
        playerHP.TakeDamage(attackDamage);
    }
}
