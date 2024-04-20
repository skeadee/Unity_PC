using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatBackground : MonoBehaviour
{
    private BoxCollider2D box;
    private float ghLength;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        ghLength = box.size.x; // �ڽ� ���� ũ��
    }

    private void Update()
    {
        if(transform.position.x < -ghLength)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        Vector2 _move = new Vector2(ghLength * 2, 0f); // ���� ��ҷ� ���� �̵��ؼ� ���ϱ� 2�� ��
        transform.position = (Vector2)transform.position + _move;
    }
}
