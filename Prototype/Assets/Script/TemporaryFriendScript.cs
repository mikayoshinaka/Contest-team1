using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ChaseChecker.cs friendCount 確認

public class TemporaryFriendScript : MonoBehaviour
{
    [SerializeField] GameObject player = default;
    [SerializeField] GameObject friendParent = default;

    GameObject object1;
    public static int friendCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(friendCount < 25)
            {              
                object1 = friendParent.transform.Find("Cube (" + friendCount + ')').gameObject;
                object1.SetActive(true);
                friendCount++;
                object1.transform.position = player.transform.position + new Vector3(2, 0, 2);
                //Debug.Log("Friend " + friendCount);
            }
        }
           
        if (Input.GetMouseButtonDown(1))
        {
            if (friendCount > 0)
            {
                friendCount--;
                object1 = friendParent.transform.Find("Cube (" + friendCount + ')').gameObject;
                object1.SetActive(false);
                //Debug.Log("Friend " + friendCount);
            }
        }
    }
}
