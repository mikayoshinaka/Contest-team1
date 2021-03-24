using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A03RotateCamera : MonoBehaviour
{
    public GameObject targetObject;
    Vector3 initiateDistance;
    // Start is called before the first frame update
    void Start()
    {
        initiateDistance = transform.position - targetObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetObject.transform.position + initiateDistance;

        if(Input.GetAxis("Horizontal2") >= 1)
        {
            transform.RotateAround(targetObject.transform.position, new Vector3(0, 1, 0), 0.5f);
        }
        if (Input.GetAxis("Horizontal2") <= -1)
        {
            transform.RotateAround(targetObject.transform.position, new Vector3(0, 1, 0), -0.5f);
        }
    }
}
