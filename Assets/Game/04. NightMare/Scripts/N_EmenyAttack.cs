using UnityEngine;

public class N_EmenyAttack : MonoBehaviour
{
    const float timeBetweenAttack = 0.5f;
    const int attackDamage = 2;

    GameObject player;
    N_PlayerHealth playerHP;
    bool playerInRange = false;
    public float timer;

    Animator ani;
    N_GameManager GameManager;

    void Awake()
    {
        player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
        playerHP = player.GetComponent<N_PlayerHealth>();
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {   
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerHP.DieCheck) ani.SetTrigger("PayerDead");


        timer += Time.deltaTime;

        if(timer >= timeBetweenAttack && playerInRange && !GameManager.GameStop)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (!playerHP.DieCheck)
        {
            playerHP.TakeDameage(attackDamage);
        }
   
    }


}
