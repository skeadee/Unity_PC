using UnityEngine;

public class Data
{
    // ���� ������ ���ڸ� ����
    public int[] Save(int[] data, string name) // ������ �迭 , �׸��� ���� �̸�
    {
        int[] s = data;
        for (int i = 0; i < s.Length; i++) PlayerPrefs.SetInt("save" + i + name, s[i]);

        return s;
    }

    public int[] Load(int[] data, string name) // �ҷ��� ������ , �����ҋ� �� �̸�
    {
        int[] s = data;
        for (int i = 0; i < s.Length; i++) s[i] = PlayerPrefs.GetInt("save" + i + name);
      
        return s;
    }
}
