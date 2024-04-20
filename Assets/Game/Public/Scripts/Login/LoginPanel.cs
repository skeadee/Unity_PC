using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour
{
    public InputField id; // ���� ���̵�
    public InputField password; // ���� �н�����
    public GameObject ErrorText;

    UserData userData = new UserData();
    void Start()
    {
        userData = new UserData();
        ErrorText.SetActive(false);
    }

    public void play(int nextScene) // play ��ư 
    {
       bool check = userData.Compare(id.text, password.text);

        if(check) // �α����� �����ϴٸ�
        {
            PlayerPrefs.SetString("NowName", id.text); // ���� �÷��� �ϰ� �ִ� �÷��̾��� �г���
            SceneManager.LoadScene(nextScene);
        }

        else // �α����� �ȵȴٸ�
        {
            ErrorCheck();
            textClear();
        }
    }

    void ErrorCheck() // �����ؽ�Ʈ ���� �Լ�
    {
        if(ErrorText.activeSelf == true) // �α����� �ȵǴ� ���·� �������� Ŭ������ ��
        {
            ErrorText.SetActive(false);
            ErrorText.SetActive(true);
        }

        else ErrorText.SetActive(true);
    }

    void textClear() // �ؽ�Ʈ Ŭ���� �Լ�
    {
        id.text = "";
        password.text = "";
    }


}
