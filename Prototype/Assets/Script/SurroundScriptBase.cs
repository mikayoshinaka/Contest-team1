using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//          メモ          //

// GameObject の PointA, PointB, PointC はまだ仮の状態なので、また他のプログラムを追加していきます

// GameObject の circle1 と circle2　は　円判定のオブジェクトです。再生に関するフラグは、これを使えばいい



public class SurroundScriptBase : MonoBehaviour
{
    [SerializeField] GameObject pointA = default;
    [SerializeField] GameObject pointB = default;
    [SerializeField] GameObject pointC = default;

    // 再生や、囲むフラグのオブジェクト
    [SerializeField] GameObject circle1 = default;
    [SerializeField] GameObject circle2 = default;

    Vector3 positionA, positionB, positionC;
    float radius1 = 0, radius2 = 0, point1 = 0, point2 = 0;

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
        if (other.name == "CubeA")
        {
            // 円の判定

            positionA = pointA.transform.position;
            positionB = pointB.transform.position;

            // Radius of Circle                         円の半径を求める

            positionC = positionA - positionB;
            if (positionC.x < 0)
            {
                positionC.x *= -1;
            }
            if (positionC.y < 0)
            {
                positionC.y *= -1;
            }
            if (positionC.z < 0)
            {
                positionC.z *= -1;
            }
            radius1 = (positionC.x * positionC.x) + (positionC.z * positionC.z);
            radius1 = Mathf.Sqrt(radius1);


            // Point of Circle                          円の transform.position を求める

            if (pointA.transform.position.x < pointB.transform.position.x)
            {
                point1 = pointA.transform.position.x;
            }
            else
            {
                point1 = pointB.transform.position.x;
            }

            if (pointA.transform.position.z < pointB.transform.position.z)
            {
                point2 = pointA.transform.position.z;
            }
            else
            {
                point2 = pointB.transform.position.z;
            }

            point1 += positionC.x / 2;
            point2 += positionC.z / 2;


            // Circle Transform

            circle1.transform.localScale = new Vector3(radius1, 1, radius1);
            circle1.transform.position = new Vector3(point1, 1, point2);
        }


        if (other.name == "CubeC")
        {
            // 円の判定

            positionA = pointC.transform.position;
            positionB = pointB.transform.position;

            // Radius of Circle                         円の半径を求める

            positionC = positionA - positionB;
            if (positionC.x < 0)
            {
                positionC.x *= -1;
            }
            if (positionC.y < 0)
            {
                positionC.y *= -1;
            }
            if (positionC.z < 0)
            {
                positionC.z *= -1;
            }
            radius2 = (positionC.x * positionC.x) + (positionC.z * positionC.z);
            radius2 = Mathf.Sqrt(radius2);


            // Point of Circle                          円の transform.position を求める

            if (pointC.transform.position.x < pointB.transform.position.x)
            {
                point1 = pointC.transform.position.x;
            }
            else
            {
                point1 = pointB.transform.position.x;
            }

            if (pointC.transform.position.z < pointB.transform.position.z)
            {
                point2 = pointC.transform.position.z;
            }
            else
            {
                point2 = pointB.transform.position.z;
            }

            point1 += positionC.x / 2;
            point2 += positionC.z / 2;


            // Circle Transform

            circle2.transform.localScale = new Vector3(radius2, 1, radius2);
            circle2.transform.position = new Vector3(point1, 1, point2);
        }
    }

}
