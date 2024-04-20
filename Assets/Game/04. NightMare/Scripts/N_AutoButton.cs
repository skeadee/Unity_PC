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
        if (GameManager.GameStop) return; // 게임 정지 상태면 버튼이 눌리지 않도록 한다

        if (check) AutoEnd(); // 자동 공격 끝 , 현재 1인칭 시점이면 자동공격 강제 종료 
        else if (!check) AutoStart(); // 자동공격 시작
    }


    public void AutoStart()
    {
        if (camerachange.change) return; // 만약 현재 1인칭 상태라면 오토 모드를 취소한다

        

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
