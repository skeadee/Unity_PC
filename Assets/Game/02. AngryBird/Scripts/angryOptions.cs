using UnityEngine;

public class angryOptions : MonoBehaviour
{
    AngryGameMananger angry;

    void Awake()
    {
        angry = GameObject.Find("AngryGamaManager").GetComponent<AngryGameMananger>();
    }

    void OnEnable()
    {
        angry.GameMode = 2;
    }

    void OnDisable()
    {
        angry.GameMode = 1;
    }

}
