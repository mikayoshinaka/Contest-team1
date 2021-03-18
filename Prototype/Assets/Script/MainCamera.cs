using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Player;
    Vector3 offset = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
            //transform.RotateAround(Player.transform.position, Vector3.up, -0.5f) ;
        //}
        //else if (Input.GetKey(KeyCode.RightShift))
        //{
            //transform.RotateAround(Player.transform.position, Vector3.up, 0.5f);
        //}
        //transform.position = Player.transform.position + offset;
        //transform.rotation = Player.transform.rotation;
    }
}
