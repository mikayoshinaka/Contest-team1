using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInsideScript : MonoBehaviour
{
    // Start is called before the first frame update

    //int bloomCheck = 0;
    //bool object1, object2;

    bool blooming, halt;
    public static bool start;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(blooming == false && object1 && object2)
        //{
        //    Debug.Log("Bloom Start");
        //    start = true;
        //    blooming = true;
        //}
        //else if(halt && !object1 && !object2)
        //{
        //    Debug.Log("Bloom End");
        //    start = false;
        //    halt = false;
        //    blooming = false;
        //}

        if (blooming == false && InsideTriggerScript.bloomCheck == 2)
        {
            start = true;
            blooming = true;
        }
        else if (halt && InsideTriggerScript.bloomCheck != 2)
        {
            start = false;
            blooming = false;
            halt = false;
            InsideTriggerScript.bloomCheck = 0;
        }
    } 

    public void BloomHalt()
    {
        Debug.Log("Bloom Halt");
        halt = true;
        InsideTriggerScript.bloomCheck = 0;
        Debug.Log(InsideTriggerScript.bloomCheck);


        //object1 = false;
        //object2 = false;
    }
}
