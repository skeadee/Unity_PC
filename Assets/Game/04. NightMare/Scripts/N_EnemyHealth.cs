using UnityEngine;
using UnityEngine.AI;

public class N_EnemyHealth : MonoBehaviour
{
    public int score; // ������ �� ���� ��ü���� ������ �ٸ� 
    public int startingHealthing = 100;
    int currentHealth;
    float sinkSpeed = 1f; // ������ ������ ������ �ӵ�

    public AudioClip deathClip;
    AudioSource audio;

    Animator ani;
    ParticleSystem hitParticles;

    CapsuleCollider cap;
    SphereCollider sph;
  

    public bool DieCheck = false; // ���� ĳ���Ͱ� �׾����� üũ�ϴ� ����

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
        currentHealth = startingHealthing; // ó�� ü�� 100
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

    void Die() // ���� ������ �ҷ����� �Լ�
    {
        DieCheck = true;

        GetComponent<NavMeshAgent>().enabled = false;
        ani.SetTrigger("Dead");

        cap.enabled = false; // �÷��̾��� �ڵ������� ������ ���ؼ� ������ �ݶ��̴� ��Ȱ��ȭ
        sph.enabled = false;

        audio.clip = deathClip;
        audio.Play();
        GameObject.Find("GameManager").GetComponent<N_GameManager>().scorePlus(score);

        Destroy(gameObject , 2f);
    }
}
