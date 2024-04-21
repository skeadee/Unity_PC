using UnityEngine;

public class  NearLayer : MonoBehaviour // ���� ����� �� �����ϰ� ������ ȸ���ϴ� ��ũ��Ʈ 
{
    public float range = 5f; // ���� ����
    public LayerMask layer; // � ���̾ ������ ���ΰ�
    public float RotationSpeed = 10f; // ȸ���ϴ� �ӵ�

    bool objCheck = false; // ������Ʈ�� ��Ÿ� �ȿ� �ִ��� üũ�ϴ� ����
    bool shootCheck = false; // �÷��̾ ���� �ٶ� ���� �ִ��� üũ�ϴ� ����

    Rigidbody playerRb;
    float shortLine = 0f;
    Vector3 sLine = new Vector3(0,0,0);

    N_PlayerShooting Shooting;

    void Start()
    {
        Shooting = GameObject.Find("GunBarrelEnd").GetComponent<N_PlayerShooting>();
    }

    void FixedUpdate()
    {
        short_Line();
        Turning(sLine);
        enemyCheck();

        if (objCheck && shootCheck) Shooting.shooting();
    }

    void short_Line()
    {
        shortLine = float.MaxValue;
        objCheck = false;

        Collider[] collider = Physics.OverlapSphere(transform.position, range, layer);

        foreach (Collider col in collider) // ���� �ȿ� �ִ� ��� ������Ʈ�� �����Ѵ�
        {  
            // ������ ������Ʈ�� ���� �Ÿ��� ��� �Ѵ�
            float distance = Vector3.Distance(gameObject.transform.position, col.gameObject.transform.position);

            if (shortLine > distance) // ������Ʈ�� �Ÿ��� ���Ͽ� ���� �Ÿ��� ����� ������Ʈ�� �Ÿ��� ���Ѵ�
            {
                shortLine = distance;
                sLine = col.transform.position - transform.position; // ���� ����� ������Ʈ�� �Ÿ�(vecotr ����)   

                objCheck = true;
            }
        }

        

    }

    void Turning(Vector3 sLine)
    {
        playerRb = GetComponent<Rigidbody>();
        sLine.y = 0f;

        if (sLine == Vector3.zero) return; // ��ǥ�� ������ ȸ�� ����

        Quaternion newPos = Quaternion.LookRotation(sLine);
        playerRb.rotation = Quaternion.Slerp(playerRb.rotation , newPos , RotationSpeed * Time.deltaTime);

     
    }

    void enemyCheck()
    {
        float angle = Vector3.Angle(transform.forward, sLine);

        if (angle < 10) shootCheck = true;
        else shootCheck = false;
    }

   


    

}
