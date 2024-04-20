using UnityEngine;
using UnityEngine.UI;

public class N_PlayerHealth : MonoBehaviour
{

    int startingHealth = 100; // �⺻ ü��
    int currentHealth;
    Slider healthSilder;
    Image damageImgae;
   
    float flashspeed = 5f;
    Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    PlyerMovement plyerMovement;
    N_PlayerShooting playerShooting;
    N_GameManager GameManager;

    AudioSource pAudio;
    public AudioClip dieAudio;
   

    bool damaged = false;
    public bool DieCheck = false; // �׾����� üũ�ϴ� ����


    void Awake()
    {
        anim = GetComponent<Animator>();
        healthSilder = GameObject.Find("HealthSlider").GetComponent<Slider>();
        damageImgae = GameObject.Find("DamageEffect").GetComponent<Image>();
        pAudio = GetComponent<AudioSource>();
        plyerMovement = GetComponent<PlyerMovement>();
        playerShooting = GetComponentInChildren<N_PlayerShooting>();
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        healthSilder.maxValue = startingHealth;
        healthSilder.value = startingHealth;
    }

    void Update()
    {
        if (DieCheck) return;

        if(damaged)
        {
            damageImgae.color = flashColor;
        }

        else
        {
            damageImgae.color =
                Color.Lerp(damageImgae.color, Color.clear, flashspeed * Time.deltaTime);    
        }

        damaged = false;

    }

    public void TakeDameage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSilder.value = currentHealth;
        pAudio.Play();

        if (currentHealth == 0 && !DieCheck) Die();
       
    }

    void Die() // �÷��̾��� ü���� 0�� ���� �� 
    {
        DieCheck = true;
        GameManager.GameStop = true; // ���� ���� ���� ����

        plyerMovement.enabled = false; // �÷��̾� �����̴� ��ũ��Ʈ ��Ȱ��ȭ 
        playerShooting.enabled = false; // �÷��̾� �� ��� ��ũ��Ʈ ��Ȱ��ȭ
        pAudio.clip = dieAudio; // �״� �Ҹ��� ����� Ŭ���� �ٲٰ� 
        pAudio.Play(); // �״� �Ҹ� ����
        GameManager.GameEnd = true;

        anim.SetTrigger("Die");  // �״� �ִϸ��̼� Ȱ��ȭ

    }

}
