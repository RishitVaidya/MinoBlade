using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotor : MonoBehaviour
{
    public Transform blade1;
    public Transform blade2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 40);

        blade1.position = transform.GetChild(0).position;
        blade2.position = transform.GetChild(1).position;
    }
}
