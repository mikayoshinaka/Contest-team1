using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A04SpawnPoint : MonoBehaviour
{
    public GameObject trakingFollow;    // トラッキング先の仲間
    public GameObject followPrefab;
    GameObject spawnFollow;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawnFollow = Instantiate(followPrefab, transform.position, Quaternion.identity);

            spawnFollow.GetComponent<A01FollowScript>().TrackingObject = trakingFollow;

            trakingFollow = spawnFollow;
        }
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
