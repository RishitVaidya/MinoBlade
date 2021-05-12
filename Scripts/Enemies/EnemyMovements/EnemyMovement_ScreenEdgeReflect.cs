using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_ScreenEdgeReflect : MonoBehaviour
{

    private float speed_Movement;

    private void Start()
    {
       

        //transform.eulerAngles = 
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles.y);
       // Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("boundary_right"))
        {

        }
    }

    public void Init(float ms)
    {
        speed_Movement = ms;
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed_Movement);

    }
}
