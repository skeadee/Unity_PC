using UnityEngine;
using UnityEngine.UI;

public class SignPanel : MonoBehaviour
{
    public InputField id , pass;
    UserData userData;
    public Text errorText; // �г� ���ʿ� �ؽ�Ʈ

    string blank = "Please enter all ID/password"; // �Է��� �����϶�
    string same = "The ID that already exists"; // ���� ���̵�� ����� ������
    string create = "Sign up is complete";

    void Start()
    {
        userData = new UserData();
    }

    public void CreateButton() // ���̵� �����ϴ� ��ư 
    {
        errorText.text = ""; // ��ư�� ���� �� ���� ���� �ʱ�ȭ

        if (id.text.Length == 0 || pass.text.Length == 0)
        {
            PanelText();
            errorText.text = blank;
            return;
        }

        bool check = userData.Compare(id.text , pass.text); // ���� ���̵�� ����� ������ �ִ� üũ

        if (check)
        {
            PanelText();
            errorText.text = same;
        }

        else // ���̵� ������ �����ϴٰ� �߰� �����
        {
            userData.Add(id.text, pass.text);
            PanelText();
            errorText.text = create;
        }
      
    }

    void PanelText()
    {
        GameObject txt = errorText.gameObject;

       if(txt.activeSelf == transform)
       {
            txt.SetActive(false);
            txt.SetActive(true);
       }

       else
       { 
            txt.SetActive(true);
       }

    }

}
