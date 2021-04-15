using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle2AdjustmentScript : MonoBehaviour
{
    string objectName;
    string[] nameSplit = new string[2];
    bool checkNum;
    int num;
    public static int passNum1, passNum2;

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
        if (other.tag == "CircleFlag")
        {
            objectName = other.name;
            nameSplit = objectName.Split('_');

            checkNum = int.TryParse(nameSplit[1], out num);

            if (num < SurroundScript.midnum)
            {
                passNum1 = num;
            }

            if (num > SurroundScript.midnum)
            {
                if(passNum2 < num)
                {
                    passNum2 = num;
                    //Debug.Log(passNum2);
                }
            }
        }
    }
}
