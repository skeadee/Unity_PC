using System.Collections;
using UnityEngine;

public class GallagGameManager : MonoBehaviour
{
   [HideInInspector] public int score = 0;
    public int life;
    public bool PauseCheck; // �ɼ�â���� �ٸ� ������ �̵��Ѵٸ� ���� â�� ����� �ʴ´�

    public GameObject[] life_img;

    public static int GameMode = 0;

    // ���� ��尡 0 �̸� ���� ���� �� 
    // ���� ��尡 1�̸� ���� ���� �ϰ� �����
    // ���� ��尡 2 �̸� ���� �Ͻ� ���� //  �ϴ� �̰� ���߿� ������ �ϵ��� ���� 
    // ���� ��� 4�̸� ��� ������ ���� �� �ٷ�����â �߰� �����

    [Space(15f)]
    public GameObject count;
    public GameObject scoreborad;
    public int i = 0;

    void Start()
    {
        i = 0;
        GameMode = 0;
        life = 3;
        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        yield return new WaitForSeconds(2f);
        count.SetActive(true);
    }

    void Update()
    {
        if(life == 0)
        {
            PlayerPrefs.SetInt("GallagScore", score);
            GameMode = 4;
        }

        if (GameMode == 4 && !scoreborad.activeSelf && !PauseCheck) scoreborad.SetActive(true);
    }

   
}
