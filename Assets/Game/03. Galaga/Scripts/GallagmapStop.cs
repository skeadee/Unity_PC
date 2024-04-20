using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagmapStop : MonoBehaviour
{
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GallagGameManager.GameMode == 4) rb2d.simulated = false;
    }
}
