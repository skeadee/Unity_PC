using UnityEngine;

public class AngryEnemy : MonoBehaviour
{
    AngryGameMananger Angry;
    Animator ani;
    Rigidbody2D rb2d;
  

    void Start()
    {
        Angry = GameObject.Find("AngryGamaManager").GetComponent<AngryGameMananger>();
        rb2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();      
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "BirdFloor" && Angry.GameMode == 1) Die();
    }


    void Die()
    {
        ani.SetTrigger("crash");
    }

    public void Des() // �� �ִϸ��̼ǿ� �ִ� �Լ�
    {
        Angry.Score += 100;
        Angry.BirdCheck--;
        Destroy(gameObject);
    }


}
