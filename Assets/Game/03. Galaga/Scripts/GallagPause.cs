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

    public void NextScene(int sceneNumber) // 씬 다음으로 전환하기
    {
        Time.timeScale = 1; // 게임 시간 되돌리기 
        GameManager.PauseCheck = true;  // 점수창 안나오게 하기
        GallagGameManager.GameMode = 4; // 게임 종료모드로 바꾸기
        loadscene.loadScene(sceneNumber); // 다음씬 실행하기
    }
}
