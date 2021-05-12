using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnusedBlade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BladeDropEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MainBlade"))
        {
            GameManager.Instance.playerController.AddBlade();
            gameObject.SetActive(false);
        }
    }
    private void BladeDropEffect()
    {
        Rigidbody this_rb = this.GetComponent<Rigidbody>();
        this_rb.AddForce(new Vector3(Random.Range(0, 1f), 0, Random.Range(0, 1f)) * 500, ForceMode.Force);
        this_rb.AddTorque(Vector3.up * 500, ForceMode.Force);
    }
}
