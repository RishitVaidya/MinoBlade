using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotorWings : Base_Enemy
{

    public RotorBoss RotorBoss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainBlade"))
        {
            Trigger_MainBlade();

           
        }

       

    }
}
