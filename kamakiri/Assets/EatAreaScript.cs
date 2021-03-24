using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAreaScript : MonoBehaviour
{
    /*eater中に当たったらeatingに変える*/
    [SerializeField] HanakamakiriScript hanakamakiriScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Death")
        {
            hanakamakiriScript.SetState(HanakamakiriScript.State.Eating);
        }
    }
}
