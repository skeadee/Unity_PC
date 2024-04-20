using UnityEngine;
using UnityEngine.UI;

public class PlaterHelath : MonoBehaviour
{
    private int startingHealth = 100;
    private int currentHealth;
    private Slider healthSlider;
    private Image damageImage;
    private AudioSource pAudio;
    private float flashSpeed = 5f;
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    private bool damaged = false;

    void Awake()
    {
        healthSlider = GameObject.Find("HealthSilder").GetComponent<Slider>();
        damageImage = GameObject.Find("DamageEffect").GetComponent<Image>();
        pAudio = GetComponent<AudioSource>();

    }

    void Start()
    {
        currentHealth = startingHealth;

        healthSlider.maxValue = startingHealth;
        healthSlider.value = startingHealth;

    }

    void Update()
    {
        if(damaged)
        {
            damageImage.color =
                Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

    }


    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        pAudio.Play();

    }
}
