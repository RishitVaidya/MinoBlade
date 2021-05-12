using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_Continous : MonoBehaviour
{
    public EnemyMovement_ContinousFollow enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            enemy.ChangeDirectionWhenObstacleInPath(other.transform.position);
            enemy.speed *= 2;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        enemy.SetPlayerTarget();

        if (other.CompareTag("Block"))
        {
            enemy.speed /= 2;
        }
            
    }
}
