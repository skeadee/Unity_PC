using UnityEngine;

public class  NearLayer : MonoBehaviour // 가장 가까운 적 감지하고 방향을 회전하는 스크립트 
{
    public float range = 5f; // 감지 범위
    public LayerMask layer; // 어떤 레이어를 감지할 것인가
    public float RotationSpeed = 10f; // 회전하는 속도

    bool objCheck = false; // 오브젝트가 사거리 안에 있는지 체크하는 변수
    bool shootCheck = false; // 플레이어가 적을 바라 보고 있는지 체크하는 변수

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

        foreach (Collider col in collider) // 범위 안에 있는 모든 오브젝트를 감지한다
        {  
            // 각각의 오브젝트에 대한 거리를 계산 한다
            float distance = Vector3.Distance(gameObject.transform.position, col.gameObject.transform.position);

            if (shortLine > distance) // 오브젝트의 거리를 비교하여 가장 거리가 가까운 오브젝트의 거리를 구한다
            {
                shortLine = distance;
                sLine = col.transform.position - transform.position; // 가장 가까운 오브젝트의 거리(vecotr 형태)   

                objCheck = true;
            }
        }

        

    }

    void Turning(Vector3 sLine)
    {
        playerRb = GetComponent<Rigidbody>();
        sLine.y = 0f;

        if (sLine == Vector3.zero) return; // 목표가 없으면 회전 금지

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
