using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_RandomPointToPoint : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    public GameObject sideTarget;

    public Vector3 randomPosition;
    public Vector3 targetPosition;


    public Transform director;


    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Start()
    {
        SetRandomPositionInRange();
        SetDefaultTarget();

        speed = Random.Range(30, 40);
    }

    void FixedUpdate()
    {
        EnemyMotion();

        if (Vector3.Distance(transform.position, randomPosition) < 1)
        {
            SetRandomPositionInRange();
        }
    }

    private void EnemyMotion()
    {
        RotateDirector();
        Move();
    }

    private void RotateDirector()
    {
        Vector3 relativePos = targetPosition - transform.position;
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
                sideTarget.transform.position = transform.position + new Vector3(-20 , 0 , 10);
            }

            else
            {
                sideTarget.transform.position = transform.position + new Vector3(-20, 0, -10);
            }

        }

        else
        {
            if (wallPosition.z > transform.position.z)
            {
                sideTarget.transform.position = transform.position + new Vector3(20, 0, 10);
            }

            else
            {
                sideTarget.transform.position = transform.position + new Vector3(20, 0, -10);
            }
        }


        SetSideTarget();
    }

    private void SetSideTarget()
    {
        Debug.Log("here");
        targetPosition = sideTarget.transform.position;
    }


    public void SetRandomPositionInRange()
    {
        Vector3 newTargetPosition = Vector3.zero;

        randomPosition.x = Random.Range(GameManager.Instance.boundaries[2].position.x + 0.45f, GameManager.Instance.boundaries[3].position.x - 0.45f);
        randomPosition.z = Random.Range(GameManager.Instance.boundaries[0].position.z + 0.45f, GameManager.Instance.boundaries[1].position.z - 0.45f);
        randomPosition.y = 0;

        SetDefaultTarget();
    }


    public void SetDefaultTarget()
    {
        targetPosition = randomPosition;
    }

    
   

}
