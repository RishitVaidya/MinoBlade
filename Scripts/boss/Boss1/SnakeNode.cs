using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnakeNode : Base_Enemy
{

    public SnakeBoss snakeBoss;

    private void Start()
    {
        Set_TextMest_HitPoints();
    }



    private void OnTriggerEnter(Collider other)
    {
        //snakeBoss.list_bodyparts[snakeBoss.list_bodyparts.Count - 1] == this
        if (other.CompareTag("MainBlade"))
        {
            if(snakeBoss.list_bodyparts[snakeBoss.list_bodyparts.Count-1] == this)
            {
                Trigger_MainBlade();

            }
            
        }

       
    }
}
