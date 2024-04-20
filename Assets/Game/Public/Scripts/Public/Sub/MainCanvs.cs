using UnityEngine;

public class MainCanvs : MonoBehaviour
{
    MainVideo video;
    RectTransform Contenet;

    int interval = 500; // ���� 
    int i; // ���� ���̴� ��ư ��ġ 

    Vector3 orgLocation; // ���� ��ġ
    Vector3 targetLocation; // ��ǥ��ġ

    public int GameCount; // ���� ���� �Է��ϱ�
    int GameValue;

  
    public float moveSpeed = 1f; // ��ư �����ϴ� �ð�
    float moveTime = 0f;

    public bool moving = false; // ���� ��ư�� �����̰� �ִ��� üũ�ϴ� ����


    void Awake()
    {
        Contenet = GameObject.Find("Content").GetComponent<RectTransform>();
        video = GameObject.Find("Video Panel").GetComponent<MainVideo>();
    }

    void Start()
    {
        orgLocation = new Vector3(0, 0, 0);
        i = 0;
        moveTime = 0f;
        GameValue = GameCount * interval;
    }

    void Update()
    {
        moveTime += Time.deltaTime;
        float t = moveTime / moveSpeed;

       Contenet.anchoredPosition = Vector2.Lerp(orgLocation, targetLocation, t);

        if (Contenet.anchoredPosition.y % interval == 0)
        {
            moving = true;
            video.videoOn(i / interval);
        }


        else
        {
            moving = false;
           
        }

        
       
    }


    public void UpButton()
    {
        if (i - interval < 0 || !moving) return;

        video.videoEnd(i / interval);
        orgLocation = new Vector3(0, i);
        moveTime = 0f;

        i -= interval;
        targetLocation = new Vector3(0, i);
    }

    public void DownButton()
    {
        if (i + interval >= GameValue || !moving) return;

        video.videoEnd(i / interval);
        orgLocation = new Vector3(0, i);
        moveTime = 0f;

        i += interval;
        targetLocation = new Vector3(0, i);
    }


}
