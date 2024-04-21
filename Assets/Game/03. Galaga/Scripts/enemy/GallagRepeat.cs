using UnityEngine;
using System.Collections;

public class GallagRepeat : MonoBehaviour
{
    int p = 0;
    GallagEnemyMove move;
    public float speed;
    public float x, y; // �� ��° ���� ���� �����ϴ� ���� 
    bool RepectParrten2Check = false; 

    Animator ani;

    void Awake()
    {
        move = GetComponent<GallagEnemyMove>();
        ani = GetComponent<Animator>();
    }

    void Start()
    {
       StartCoroutine(Repeat(0.1f, 0, 40)); // �¿� �ݺ� 
    }


    public void RepectParrten2() // �� �Ʒ��� �Դ� ���� �ϴ� ���� �����ϱ�
    {
        StopAllCoroutines();
        StartCoroutine(RepectParrten());
    }

    IEnumerator RepectParrten()
    {
        yield return center(0.1f , 0);   //Repeat(0.1f, 0, 40) �̰Ŷ� �� �Ȱ��� �ϱ�
        RepectParrten2Check = true;
        StartCoroutine(Repeat(x , y , 10));
    }


    IEnumerator center(float x , float y)
    {        
        if(p != 0)
        {
            int max = p;

            for (int i = 0; i < max; i++)
            {
                p--;
                move.loc += new Vector3(x, y, 0);
                yield return new WaitForSeconds(0.01f * speed);
            }

        }

    }




    IEnumerator Repeat(float x, float y, int max)
    {
        for (int i = 0; i < max; i++)
        {
            p++;
            move.loc += new Vector3(x, y, 0);
            yield return new WaitForSeconds(0.01f * speed);

           
        }

        for (int i = 0; i < max; i++)
        {
            p--;
            move.loc += new Vector3(-x, -y, 0);
            yield return new WaitForSeconds(0.01f * speed);

           
        }


      if(!RepectParrten2Check) StartCoroutine(Repeat(-x, -y, max));
      else StartCoroutine(Repeat(x, y, max));
    }


    void Update()
    {
        if (GallagGameManager.GameMode == 4)
        {
            ani.SetBool("stop", true);
            StopAllCoroutines();
        }
    }


}
