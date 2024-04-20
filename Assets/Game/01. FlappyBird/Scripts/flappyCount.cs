using UnityEngine;

public class flappyCount : MonoBehaviour
{
   
    void OnDestroy()
    {
        GameObject.Find("GameManager").GetComponent<FlappyGameManager>().GameStart();
    }

}
