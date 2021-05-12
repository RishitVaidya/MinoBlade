using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_Random : MonoBehaviour
{
    public EnemyMovement_RandomPointToPoint enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            enemy.ChangeDirectionWhenObstacleInPath(other.transform.position);
            enemy.speed *= 2;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            if (enemy.targetPosition.Equals(enemy.randomPosition))
            {
                enemy.ChangeDirectionWhenObstacleInPath(other.transform.position);
            }
            
            //enemy.speed *= 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            SetDefault();
            enemy.speed /= 2;
        }

    

    }

    private void SetDefault()
    {
        enemy.SetDefaultTarget();
    }
}
