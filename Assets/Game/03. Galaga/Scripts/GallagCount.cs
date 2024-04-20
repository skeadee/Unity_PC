using UnityEngine;

public class GallagCount : MonoBehaviour
{

    GallagGameManager gallag;
    public GameObject NextText;

    void OnDestroy()
    {
        NextText.SetActive(true);
    }
}
