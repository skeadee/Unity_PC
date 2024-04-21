using UnityEngine;

public class UserData :MonoBehaviour
{   
    int maxUserData = 100;
    string[] id_Data;
    string[] pass_Data;

    void Awake()
    {
        maxUserData = 100;
        id_Data = new string[maxUserData];
        pass_Data = new string[maxUserData];
        Load();
    }

    public UserData() // ������ , �ٸ� ������ �ν���Ʈ ������ �ڵ����� ����Ǵ� �Լ�
    {
        maxUserData = 100;
        id_Data = new string[maxUserData];
        pass_Data = new string[maxUserData];
        //Load();
    }

   

    public void Add(string id, string password) // ���⼭ ���ο� ���������͸� �߰��Ѵ�.
    {
        for (int i = 0; i < maxUserData; i++)
        {
            
            if (id_Data[i] == null)
            {
                id_Data[i] = id;
                pass_Data[i] = password;

                break;
            }
            
        }

        Save();
    }

    public void Save() // ���⼭ ���������͸� �����Ѵ�.
    {
        for (int i = 0; i < maxUserData; i++)
        {
            if (id_Data[i] == null) break;

            PlayerPrefs.SetString("id" + i, id_Data[i]);
            PlayerPrefs.SetString("password" + i, pass_Data[i]);
        }

        //Debug.Log("������ ���̺� ��");
    }

    public void Load() //  ���⼭ �����͸� �ҷ� �´�
    {
        for (int i = 0; i < maxUserData; i++)
        {
            string id = PlayerPrefs.GetString("id" + i, "");
            string pass = PlayerPrefs.GetString("password" + i, "");

            if (id == "") break;

            id_Data[i] = id;
            pass_Data[i] = pass;
        }

       // Debug.Log("������ �ε� ��");
    }


    //���� �����Ͱ� �ִ��� Ȯ���ϴ� �Լ�
    public bool Compare(string id , string pass) // ���� �÷��̾��� ���̵�� �н����带 �Է� �޴´�
    {
        Load(); // ���� ����Ǿ� �ִ� �����͸� �ҷ�����

        bool sameCheck = false; // ���� �̸��� �ִ��� üũ �ϴ� ����

        for(int i=0 ; i < maxUserData ; i++)
        {
            if (id == id_Data[i] && pass == pass_Data[i])
            {
                sameCheck = true;
                break;
            }
       
        }

         return sameCheck; // true�� ���̵� �ְ� false�� ���̵� ����
    }


}
