using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_DiscreteFollow : MonoBehaviour
{

    private float speed_Movement;
    private Transform targetTransform;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.Equals(targetPosition))
        {
            ChangeDirection();
        }

    }

    public void Init(float ms, Transform playerTransform)
    {
        speed_Movement = ms;
        targetTransform = playerTransform;
        ChangeDirection();
    }

    private void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed_Movement);
    }

    private void ChangeDirection()
    {

        targetPosition = targetTransform.position;
        //transform.LookAt(targetTransform.position);

    }
}
