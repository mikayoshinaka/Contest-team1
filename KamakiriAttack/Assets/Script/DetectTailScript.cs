using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTailScript : MonoBehaviour
{
    public ExitFormationScript myExitFormationScript;
    [SerializeField] GameObject exitRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tail")
        {
            if(exitRange.name == "ExitRange1")
            {
                myExitFormationScript.CountdownStart1();
            }
            else if (exitRange.name == "ExitRange2")
            {
                myExitFormationScript.CountdownStart2();
            }
        }

        if ((other.tag == "Player" || other.tag == "CircleFlag") && other.tag != "Tail")
        {
            if (exitRange.name == "ExitRange1" && ExitFormationScript.countdown1)
            {
                myExitFormationScript.CountdownReset1();
            }
            else if (exitRange.name == "ExitRange2" && ExitFormationScript.countdown2)
            {
                myExitFormationScript.CountdownReset2();
            }
        }
    }
}
