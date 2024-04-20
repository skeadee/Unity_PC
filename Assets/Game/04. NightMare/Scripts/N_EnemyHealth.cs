using UnityEngine;
using UnityEngine.AI;

public class N_EnemyHealth : MonoBehaviour
{
    public int score; // 죽을때 줄 점수 개체마다 점수가 다름 
    public int startingHealthing = 100;
    int currentHealth;
    float sinkSpeed = 1f; // 죽을때 땅으로 꺼지는 속도

    public AudioClip deathClip;
    AudioSource audio;

    Animator ani;
    ParticleSystem hitParticles;

    CapsuleCollider cap;
    SphereCollider sph;
  

    public bool DieCheck = false; // 현재 캐릭터가 죽었는지 체크하는 변수

    void Awake()
    {  
        ani = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        cap = GetComponent<CapsuleCollider>();
        sph = GetComponent<SphereCollider>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        currentHealth = startingHealthing; // 처음 체력 100
    }

    void FixedUpdate()
    {
        if (DieCheck) transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }


    public void TakeDamge(int damage  , Vector3 hitPoint)
    {
        currentHealth -= damage;
        audio.Play();

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();


        if (currentHealth <= 0 && !DieCheck) Die();
    }

    void Die() // 적이 죽을때 불러오는 함수
    {
        DieCheck = true;

        GetComponent<NavMeshAgent>().enabled = false;
        ani.SetTrigger("Dead");

        cap.enabled = false; // 플레이어의 자동공격의 감지를 위해서 죽으면 콜라이더 비활성화
        sph.enabled = false;

        audio.clip = deathClip;
        audio.Play();
        GameObject.Find("GameManager").GetComponent<N_GameManager>().scorePlus(score);

        Destroy(gameObject , 2f);
    }
}
