using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotorBoss : Base_Enemy
{

    public Transform rotor;

    public List<RotorWings> list_allChild;

    

    // Start is called before the first frame update
    void Start()
    {
        Set_TextMest_HitPoints();
    }

    // Update is called once per frame

    void Update()
    {
        rotor.Rotate(Vector3.up * Time.deltaTime * 20);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainBlade"))
        {

            if(list_allChild.Count == 0)
            {
                Trigger_MainBlade();
            }
            

           
        }

       

    }

}
