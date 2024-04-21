using UnityEngine;

public class MainButton : MonoBehaviour
{
    public RectTransform LoginPanel;
    public RectTransform SignPanel;


    public float timer = 2f; // ������Ʈ�� Ÿ�ٿ� �����ϴ� �ð�
    Vector3 target; // UIâ ��������

    float moveTime = 0;

    Vector3 UI; // ��ǥ�� �Ǵ� ui ��ġ , ui�� �����̱� �����ϴ� ��ġ
    Vector3 LoginStart = new Vector3(0, 1100f, 0); // �α��� â ó�� ��ġ
    Vector3 SignStart = new Vector3(0, -1100f, 0); // ȸ������â ó�� ��ġ
    Vector3 center = Vector3.zero; // ȭ�� �߾�

    bool panelCheck = false; // ���� �α��� â�� �����̰� �ִ��� üũ�ϴ� ����
    bool logCheck = false; // �α���â üũ ����
    bool signCheck = false; // ȸ������â üũ ����

    RectTransform objRect; // ������Ʈ ������ ��ġ�� ������ ������Ʈ


    void Start()
    {
        UI = LoginStart;
        target = LoginStart;

      
    }

    public void LogIn() // �α���â�� ����� �ҷ� ���� �Լ�
    {
        if (!panelCheck) return; // �г�â�� �����̴� ���̸� ����

        objRect = LoginPanel; // ������ ������Ʈ ���� 
        UI = objRect.anchoredPosition; // ������Ʈ�� �����̱� ������ ��ġ ����
        moveTime = 0f; // �ð� �ʱ�ȭ 

        if(!logCheck) { target = center; logCheck = true; }
        else{ target = LoginStart; logCheck = false; }  
    }

    public void SignIn()
    {
        if (!panelCheck) return; 

        objRect = SignPanel;
        UI = objRect.anchoredPosition;
        moveTime = 0f;

        if(!signCheck){ target = center; signCheck = true; }
        else { target = SignStart; signCheck = false; }        
    }


    void Update()
    {
        moveTime += Time.deltaTime;
        float t = moveTime / timer;

        if(objRect != null) objRect.anchoredPosition = Vector3.Lerp(UI , target , t);

        if (t > 1) panelCheck = true; // â�� �������� ������
        else if(t < 1) panelCheck = false; // â�� �����̴� ���̴�
    }


}
