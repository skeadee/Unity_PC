using UnityEngine;

public class GallagAni : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = this.GetComponent<Animator>();
        
    }

    public void Des()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }


    void Update()
    {
        if (GallagGameManager.GameMode == 1) animator.speed = 1;
        else animator.speed = 0;
    }



}
