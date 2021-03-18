using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseChecker : MonoBehaviour
{
    [SerializeField] Material color2;
    GameObject friend;
    CubeFollow CF;

    // Start is called before the first frame update
    void Start()
    {
        friend = transform.parent.gameObject;
        CF = friend.GetComponent<CubeFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("aaa");
            friend.GetComponent<Renderer>().material.color = color2.color;
            CF.tracking = true;
        }
    }
}
