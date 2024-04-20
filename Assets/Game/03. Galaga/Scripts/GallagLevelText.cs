using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GallagLevelText : MonoBehaviour
{
    public Text leveltxt;
    int i = 1;

    public GameObject scoreborad;
    public GameObject firsLevel;

    void Start()
    {
        StartCoroutine(NextLeveltxt(firsLevel));
    }


    public void Next(GameObject Level) { StartCoroutine(NextLeveltxt(Level)); }
    public void Finish() { StartCoroutine(FinshLeveltxt()); }


    IEnumerator NextLeveltxt(GameObject Level)
    {
        leveltxt.text = "Level " + i++;
        yield return new WaitForSeconds(2f);
        leveltxt.text = "";
        GallagGameManager.GameMode = 1;
        Level.SetActive(true);
    }

    IEnumerator FinshLeveltxt()
    {
        leveltxt.text = "Clear!";
       
        yield return new WaitForSeconds(2f);

        GallagGameManager.GameMode = 4;
        leveltxt.text = "";
        scoreborad.SetActive(true);
    }

     void Update()
     {
        if (GallagGameManager.GameMode == 4) StopAllCoroutines();
     }


}
