using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTriggerScript : MonoBehaviour
{
    public static int bloomCheck = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "flower")
        {
            if (bloomCheck < 2)
            {
                Debug.Log(bloomCheck);
                bloomCheck++;
                Debug.Log(bloomCheck);
            }
        }

        if (other.tag == "mushroom")
        {
            if (bloomCheck < 2)
            {
                Debug.Log(bloomCheck);
                bloomCheck++;
                Debug.Log(bloomCheck);
            }
        }

        //if (other.tag == "flower")
        //{
        //    if(object1 == false)
        //    {
        //        object1 = true;
        //        Debug.Log("object1 true");
        //    }
        //}

        //if (other.tag == "mushroom")
        //{
        //    if (object1 == false)
        //    {
        //        object2 = true;
        //        Debug.Log("object2 true");
        //    }
        //}
    }

}
