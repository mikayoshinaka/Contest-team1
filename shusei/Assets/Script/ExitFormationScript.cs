using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFormationScript : MonoBehaviour
{
    float timer1 = 0f, timer2 = 0f;
    float timeLimit;
    public static bool countdown1, countdown2;
    //[SerializeField] GameObject exitRange1 = default;
    //[SerializeField] GameObject exitRange2 = default;

    public SurroundScript mySurroundScript;

    // Start is called before the first frame update
    void Start()
    {
        timeLimit = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown1)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= timeLimit)
            {
                countdown1 = false;
                mySurroundScript.DisbandFormation1();
            }
        }

        if (countdown2)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= timeLimit)
            {
                countdown2 = false;
                mySurroundScript.DisbandFormation1();
            }
        }
    }


    public void CountdownStart1()
    {
        timer1 = 0;
        countdown1 = true;
        //Debug.Log("Countdown 1 Start");

        if(SurroundScript.tailChase == false && !SurroundScript.END)
        {
            //Debug.Log("TAIL TRUE");
            SurroundScript.tailChase = true;
        }
    }

    public void CountdownStart2()
    {
        timer2 = 0;
        countdown2 = true;
        //Debug.Log("Countdown 2 Start");
    }

    public void CountdownReset1()
    {
        timer1 = 0;
        countdown1 = false;
        //Debug.Log("Countdown 1 Reset");
    }

    public void CountdownReset2()
    {
        timer2 = 0;
        countdown2 = false;
        //Debug.Log("Countdown 2 Reset");
    }
}
