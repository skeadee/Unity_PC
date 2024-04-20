using UnityEngine;
using UnityEngine.UI;

public class N_AutoButton : MonoBehaviour
{
    PlyerMovement playerMove;
    NearLayer nearLayer;
    N_PlayerShooting shooting;
    Image imageColor;
    N_CameraChange camerachange;
    N_GameManager GameManager;


    bool check = false;

    

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        camerachange = player.GetComponent<N_CameraChange>();
        GameManager = GameObject.Find("GameManager").GetComponent<N_GameManager>();
        playerMove = player.GetComponent<PlyerMovement>();
        nearLayer = player.GetComponent<NearLayer>();
        shooting = GameObject.Find("GunBarrelEnd").GetComponent<N_PlayerShooting>();
        imageColor = GetComponent<Image>();
    }

    public void Button() 
    {
        if (GameManager.GameStop) return; // ���� ���� ���¸� ��ư�� ������ �ʵ��� �Ѵ�

        if (check) AutoEnd(); // �ڵ� ���� �� , ���� 1��Ī �����̸� �ڵ����� ���� ���� 
        else if (!check) AutoStart(); // �ڵ����� ����
    }


    public void AutoStart()
    {
        if (camerachange.change) return; // ���� ���� 1��Ī ���¶�� ���� ��带 ����Ѵ�

        

        playerMove.TurningOn = false;
        check = true;
        nearLayer.enabled = true;
        imageColor.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }

    public void AutoEnd()
    {
        

        playerMove.TurningOn = true;
        check = false;
        nearLayer.enabled = false;
        imageColor.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
    }


   


}
