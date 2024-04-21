using UnityEngine;

public class LoadScene : MonoBehaviour
{
    GameObject fade_In, fade_Out;

    void Start()
    {
        fade_In = Resources.Load<GameObject>("Fade/Fade In");
        fade_Out = Resources.Load<GameObject>("Fade/Fade Out");

        Fade_Out();
    }

    public void loadScene(int sceneNumber) // �� �̵��� Fade_In�� �۵��ϴ� �Լ�
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("NextScene", sceneNumber);
        Fade_In();     
    }


    public void Fade_In() // �� ���� �� �۵�
    {
       Instantiate(fade_In);    
    }

    public void Fade_Out() // �� ���۽� �۵� 
    {
       Instantiate(fade_Out);
    }

 
}
