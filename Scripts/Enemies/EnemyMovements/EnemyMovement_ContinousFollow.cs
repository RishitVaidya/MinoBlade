using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_ContinousFollow : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

    // Specify the target for the enemy.
    public GameObject target;
    public GameObject sideTarget;


    public Transform director;

    private void Start()
    {
        SetPlayerTarget();

        speed = Random.Range(30, 40);
    }

    void FixedUpdate()
    {
        EnemyMotion();
    }

    private void EnemyMotion()
    {
        RotateDirector();
        Move();
    }

    private void RotateDirector()
    {
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        director.rotation = Quaternion.Slerp(director.rotation, rotation, Time.deltaTime * 5);
    }

    private void Move()
    {
        GetComponent<Rigidbody>().velocity = director.forward * Time.deltaTime * speed;
    }

    public void ChangeDirectionWhenObstacleInPath(Vector3 wallPosition)
    {
        float xDistance = Mathf.Abs(transform.position.x - wallPosition.x);

        if (wallPosition.x > transform.position.x)
        {
            if (wallPosition.z > transform.position.z)
            {
                sideTarget.transform.position = transform.position + Vector3.left + Vector3.forward;
            }

            else
            {
                sideTarget.transform.position = transform.position + Vector3.left + Vector3.back;
            }

        }

        else
        {
            if (wallPosition.z > transform.position.z)
            {
                sideTarget.transform.position = transform.position + Vector3.right + Vector3.forward;
            }

            else
            {
                sideTarget.transform.position = transform.position + Vector3.right + Vector3.back;
            }
        }


        SetSideTarget();
    }

    private void SetSideTarget()
    {
        target = sideTarget;
    }

    public void SetPlayerTarget()
    {
        target = GameManager.Instance.playerController.gameObject;
    }

}


