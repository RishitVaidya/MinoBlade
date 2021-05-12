using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBoss : MonoBehaviour
{
    public List<SnakeNode> list_bodyparts;

    public Vector3 targetPoint;
    Vector3 targetDirection;
    Quaternion targetDir;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeRandomDirectionContinously());
    }

    // Update is called once per frame
    void Update()
    {

        if(list_bodyparts.Count > 0)
        {
            list_bodyparts[0].transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        

        list_bodyparts[0].transform.rotation = targetDir;

        for (int i = 1; i < list_bodyparts.Count; i++)
        {
            Vector3 targetPosition = list_bodyparts[i - 1].transform.TransformPoint(new Vector3(0, 0, -1.2f));

            list_bodyparts[i].transform.position = Vector3.Slerp(list_bodyparts[i].transform.position, targetPosition, Time.deltaTime * 12);
            list_bodyparts[i].transform.LookAt(list_bodyparts[i - 1].transform);



        }
    }


    private void GenerateRandomDirection()
    {
        targetPoint.x = Random.Range(-3.5f, 3.5f);
        targetPoint.z = Random.Range(-6, 6);

        targetDir = Quaternion.LookRotation(targetPoint - list_bodyparts[0].transform.position);
    }

    IEnumerator ChangeRandomDirectionContinously()
    {
        while (true)
        {
            GenerateRandomDirection();
            yield return new WaitForSeconds(Random.Range(0.7f, 2f));
        }
    }
}
