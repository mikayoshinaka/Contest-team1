using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ChaseChecker.cs friendCount 確認

public class TemporaryFriendScript : MonoBehaviour
{
    [SerializeField] GameObject player = default;
    [SerializeField] GameObject friendParent = default;
    [SerializeField] GameObject tail;

    GameObject object1;
    public static int friendCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if(friendCount < 50)
            {
                object1 = friendParent.transform.Find("Cube (" + friendCount + ')').gameObject;
                tail.transform.parent = object1.transform;
                tail.transform.position = object1.transform.position;
                object1.SetActive(true);
                friendCount++;
                object1.transform.position = player.transform.position + new Vector3(2, 0, 2);
            }
        }
           
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (friendCount > 0)
            {
                friendCount--;
                if (friendCount > 0)
                {
                    object1 = friendParent.transform.Find("Cube (" + (friendCount - 1) + ')').gameObject;
                    tail.transform.parent = object1.transform;
                    tail.transform.position = object1.transform.position;
                }
                object1 = friendParent.transform.Find("Cube (" + friendCount + ')').gameObject;
                object1.SetActive(false);
            }
        }
    }
}
