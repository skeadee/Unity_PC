using UnityEngine;

public class GallagDestory : MonoBehaviour
{
    int all;
    GallagLevelText NextLevelStart; 

    public bool Main;
    public bool serve;

    [Space(20f)]
    public bool LastLevel = false;
    public GameObject NextLevel;

    void Start()
    {
        NextLevelStart = GameObject.Find("NextLevelText").GetComponent<GallagLevelText>();
        all = gameObject.transform.childCount;

    
    }

    void Update()
    {

       if(Main)
       {
           
            if (gameObject.transform.childCount == 0) Destroy(gameObject);
       }
     
       else if(serve)
       {
            int check = 0;

            for (int i = 0; i < all; i++)
            {
                if (!gameObject.transform.GetChild(i).gameObject.activeSelf) check++;
            }

            if (check == all) Destroy(gameObject);
       }

        

    }



    void OnDestroy()
    {
        if (LastLevel)
        {
            GallagGameManager.GameMode = 4;
            NextLevelStart.Finish();
        }

        else if (NextLevel != null)
        { 
            NextLevelStart.Next(NextLevel);
        }
    }


}
