using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class HorzScroll : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public float speed = -2f;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Move()
    {
        _rb2d.velocity = new Vector2(speed, 0f);
    }

    public void Stop()
    {
        _rb2d.velocity = Vector2.zero;
    }


}
