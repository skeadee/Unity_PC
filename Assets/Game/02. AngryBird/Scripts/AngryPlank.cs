using UnityEngine;

public class AngryPlank : MonoBehaviour
{
    AngryGameMananger GameManager;
    Rigidbody2D rb2d;
    void Start()
    {
        GameManager = GameObject.Find("AngryGamaManager").GetComponent<AngryGameMananger>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    
}
