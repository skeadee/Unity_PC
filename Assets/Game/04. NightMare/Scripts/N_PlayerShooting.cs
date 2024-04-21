using UnityEngine;

public class N_PlayerShooting : MonoBehaviour
{
    LineRenderer bulletLine;
    Light light;
    AudioSource audio;
    N_GameManager GameManager;

    public float bulletLength = 100f; // �Ѿ� ���� 
    int bulletDamage = 20; // �Ѿ��� ������
    int Layer;

    float bulletDelay = 0.1f;

    Ray ray;
    RaycastHit rayhit;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
            
        bulletLine = GetComponent<LineRenderer>();
        light = GetComponent<Light>();
        audio = GetComponent<AudioSource>();

        bulletDelay = 0.1f;

        Layer = LayerMask.GetMask("Shootable");
    }

    void FixedUpdate()
    {
        if (GameManager.GameStop || GameManager.UICheck) return; // ������ ���� ���¸� �� ��°� �������� �ʴ´� , ���콺�� ui���� �ö� �ִٸ� ���� ����

        if (bulletDelay > 0) // �Ѿ� �i���� �����̰� 0�� �ƴ϶�� ���� ��
        {
            bulletDelay -= Time.deltaTime;
            bulletEnd();
            return;
        }

        if (Input.GetMouseButton(0) && !GameManager.UICheck)
        {
            shooting();
        }
    }

    public void shooting()
    {
        if (bulletDelay > 0) return;

        bulletLine.enabled = true; // ���η������� Ȱ��ȭ�Ͽ� �Ѿ� ���� ���� ���̵��� �����
        light.enabled = true; // Light������Ʈ�� Ȱ��ȭ ���� �Ѿ� ���� ���̰� �����
        audio.Play(); // �� ��� �Ҹ��� Ȱ��ȭ

        ray.origin = transform.position; // Ray�� �߻� ��ġ�� ���� ������Ʈ ��ġ���� �߻� �ϵ��� �����
        ray.direction = transform.forward; // Ray�� �߻� ������ ���� ������Ʈ�� z������ �߻��ϵ��� �����Ѵ�

        bulletLine.SetPosition(0, transform.position); // �Ѿ� �߻� ��ġ�� ���� �� ��ġ�� �������Ѵ� 


        if (Physics.Raycast(ray , out rayhit , bulletLength , Layer)) // Ray�� �߻� ������ �� ���̾ �浹�Ѵٸ�
        {
            bulletLine.SetPosition(1, rayhit.point); // �Ѿ��� ���̸� �� ���� ���� ��ġ������ ������ �Ѵ�

            N_EnemyHealth enemyHealth = rayhit.collider.GetComponent<N_EnemyHealth>(); // ���� ���� �¾Ҵٸ� ��ũ��Ʈ ���� 
            if (enemyHealth != null) enemyHealth.TakeDamge(bulletDamage , rayhit.transform.position); // ���� ���� ���ȿ� ��ũ��Ʈ�� �ִٸ� ����� hp�� ��´�.


        }

        else // Ray�� ���� ���� �ʾҴٸ� �׳� �ִ� ��Ÿ�(100f)���� ���� �����Ѵ�.
        {
            bulletLine.SetPosition(1 , ray.origin + ray.direction * bulletLength);

            // bulletLine.SetPosition(1 , ray.direction * bulletLength); �� �ڵ嵵 ���������� ������ �Ǵ��� �������� ���� �����Կ��� �����
              
        }

        bulletDelay = 0.1f; // �Ѿ� ������ �缳��
        
    }


    void bulletEnd() // �Ѿ��� �߻� �� ��Ȱ��ȭ �ؾ��ϴ� ��
    {
        bulletLine.enabled = false;
        light.enabled = false;
    }


    void OnDisable() // ���� ������ �����µ� �Ѿ� ������ �Ȼ�� �� ��� ���ִ� ��
    {
        bulletLine.enabled = false;
    }
}
