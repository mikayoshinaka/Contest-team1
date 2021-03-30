using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    float gear = 260.0f;
    Vector3 latestPos;
    // Start is called before the first frame update
    void Start()
    {
        latestPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("speed" + speed * (float)Time.deltaTime * gear * new Vector3(0.1f, 0, 0));


        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.position += speed * (float)Time.deltaTime * gear * new Vector3(0.1f, 0, 0);
            //moving = true;

        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.position += speed * (float)Time.deltaTime * gear * new Vector3(-0.1f, 0, 0);
            //moving = true;

        }
        if (Input.GetAxis("Vertical") > 0)
        {
            transform.position += speed * (float)Time.deltaTime * gear * new Vector3(0, 0, 0.1f);
            //moving = true;

        }
        if (Input.GetAxis("Vertical") < 0)
        {
            transform.position += speed * (float)Time.deltaTime * gear * new Vector3(0, 0, -0.1f);
            //moving = true;

        }

        Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position;  //前回のPositionの更新

        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
        }
    }
}