using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainBlade"))
        {
            GameManager.Instance.playerController.Dead();
            Instantiate(GameManager.Instance.go_bombCollectEffect, transform.position, GameManager.Instance.go_bombCollectEffect.transform.rotation);
            Destroy(gameObject);
        }
            
    }
}
