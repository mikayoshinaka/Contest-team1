using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTriggerScript : MonoBehaviour
{
    //public static int bloomCheck = 0;
    // Start is called before the first frame update

    public static bool circle1, circle2;
    public static bool bloom;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (circle1 && circle2)
        {
            if(bloom == false)
            {
                //Debug.Log("Bloom");
            }
            bloom = true;
        }
        else
        {
            bloom = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name == "Circle1")
        {
            if(other.tag == "flower" || other.tag == "mushroom")
            {
                circle1 = true;
                //Debug.Log("Circle 1 = " + circle1);
            }
        }

        if (this.gameObject.name == "Circle2")
        {
            if (other.tag == "flower" || other.tag == "mushroom")
            {
                circle2 = true;
                //Debug.Log("Circle 2 = " + circle2);
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (this.gameObject.name == "Circle1")
    //    {
    //        if (other.gameObject.tag == "flower" || other.gameObject.tag == "mushroom")
    //        {
    //            circle1 = false;
    //            Debug.Log("Circle 1 = " + circle1);
    //        }
    //    }

    //    if (this.gameObject.name == "Circle2")
    //    {
    //        if (other.gameObject.tag == "flower" || other.gameObject.tag == "mushroom")
    //        {
    //            circle2 = false;
    //            Debug.Log("Circle 2 = " + circle2);
    //        }
    //    }
    //}
}
