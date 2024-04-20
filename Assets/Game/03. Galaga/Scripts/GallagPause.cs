using UnityEngine;

public class GallagPause : MonoBehaviour
{
    LoadScene loadscene;
    bool check = false;
    public GameObject PausePanel;
    GallagGameManager GameManager;

    void Start()
    {
        GameObject p = GameObject.Find("GameManager");

        loadscene = p.GetComponent<LoadScene>();
        GameManager = p.GetComponent<GallagGameManager>();
    }

    public void stopCheck()
    {
        if (GallagGameManager.GameMode == 4) return;

        if (!check)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            check = true;
        }

        else
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            check = false;
        }
    }

    public void NextScene(int sceneNumber) // �� �������� ��ȯ�ϱ�
    {
        Time.timeScale = 1; // ���� �ð� �ǵ����� 
        GameManager.PauseCheck = true;  // ����â �ȳ����� �ϱ�
        GallagGameManager.GameMode = 4; // ���� ������� �ٲٱ�
        loadscene.loadScene(sceneNumber); // ������ �����ϱ�
    }
}
