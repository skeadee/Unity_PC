using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GallagUI : MonoBehaviour
{
    GallagGameManager gallag;
    
    public Text score;

    public Text Level;
    int level = 0;
   
    void Start()
    {
        gallag = GetComponent<GallagGameManager>();
    }

    void Update()
    {
        score.text = "Score : " + gallag.score;
    }

    
    public IEnumerator LeveText()
    {
        Level.text = "Level[" + (++level) + "]";
        yield return new WaitForSeconds(1f);
        Level.text = "";
    }

}
