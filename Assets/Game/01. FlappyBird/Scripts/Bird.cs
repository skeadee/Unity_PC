using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bird : MonoBehaviour
{
    public float upForce = 200f;
    public bool crash;

    Rigidbody2D _rb2d;
    PolygonCollider2D pol;
    Animator ani;
    AudioSource Audio;
    bool GameOver;
    Vector2 Velocity;

    FlappyGameManager GameManager;

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<FlappyGameManager>();
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        _rb2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        pol = GetComponent<PolygonCollider2D>();

        crash = false;
    }

    public void crash_off() 
    {
        if (GameOver) return;

        crash = false;
    }

    public void Die() 
    {
        GameOver = true;
        pol.isTrigger = false;
        ani.SetBool("Die", true);
    }

   
    void Jump() 
    {
        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(new Vector2(0, upForce));
        ani.SetTrigger("Nor");
    }

    void Crash_Column()
    {
        if (crash) return;

        ani.SetTrigger("Crash");
        GameManager.Hit(1);
        crash = true;
        Audio.Play();

        Invoke("crash_off", 1.5f);
    }

    void Crash_Ground()
    {
        GameManager.Hit(99);
    }

    public void GameStop()
    {
        Velocity = _rb2d.velocity;
        _rb2d.bodyType = RigidbodyType2D.Static;
        ani.enabled = false;
    }


    public void GameStart()
    {
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
        _rb2d.velocity = Velocity;
        ani.enabled = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameOver) Jump();
    }


    void OnTriggerEnter2D(Collider2D col) // 기둥과 충돌 할때
    {
        if (col.gameObject.name == "Column") Crash_Column();
        if (col.gameObject.name == "Goal" && !crash) GameManager.ScoreAdd(100);
    }


    void OnCollisionEnter2D() // 땅과 충돌 할때 
    {
        Crash_Ground();
    }

}
