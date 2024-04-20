using UnityEngine;
using UnityEngine.UI;

public class N_PlayerHealth : MonoBehaviour
{

    int startingHealth = 100; // 기본 체력
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
    public bool DieCheck = false; // 죽었을때 체크하는 변수


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

    void Die() // 플레이어의 체력이 0이 됬을 때 
    {
        DieCheck = true;
        GameManager.GameStop = true; // 게임 정지 변수 실행

        plyerMovement.enabled = false; // 플레이어 움직이는 스크립트 비활성화 
        playerShooting.enabled = false; // 플레이어 총 쏘는 스크립트 비활성화
        pAudio.clip = dieAudio; // 죽는 소리로 오디오 클립을 바꾸고 
        pAudio.Play(); // 죽는 소리 실행
        GameManager.GameEnd = true;

        anim.SetTrigger("Die");  // 죽는 애니메이션 활성화

    }

}
