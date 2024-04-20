using UnityEngine;

public class Column : MonoBehaviour
{
    FlappyGameManager flappy;
    flappyOptions options;

    public GameObject column;
    public GameObject[] obj;
    int count = 0;
    Vector3 loc = new Vector3(30, -30, 0);

    public float Column_Dealy;
    bool Stop;
    
    private void Awake()
    {
        flappy = GameObject.Find("GameManager").GetComponent<FlappyGameManager>();
        options = GameObject.Find("Pause").GetComponent<flappyOptions>();
    }


    void Start()
    {
        Column_Dealy = 2f;
        count = 0;
        Stop = true; 
        obj = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            obj[i] = Instantiate(column, loc, Quaternion.identity);
            options.horzScroll.Add(obj[i].GetComponent<HorzScroll>());
        }

    }

    public void GameStop()
    { 
        Stop = true;
    }

    public void GameStart()
    {
        Stop = false;
    }

 
    void colums_Set()
    {
        Vector3 loc = new Vector3(10, 0);
        float y = Random.Range(-0.5f, 3f);
        loc.y = y;

        obj[(count++) % 5].transform.position = loc;
        Column_Dealy = Random.Range(2f, 3f);
    }

    void FixedUpdate()
    {
        if (Stop) return;

        Column_Dealy -= Time.deltaTime;
        if (Column_Dealy < 0) colums_Set();
    }




}
