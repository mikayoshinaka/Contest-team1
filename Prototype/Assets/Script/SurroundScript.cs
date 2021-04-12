using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//          メモ          //

// GameObject の circle1 と circle2　は　円判定のオブジェクトです。再生に関するフラグは、これを使う



public class SurroundScript : MonoBehaviour
{
    // 再生や、囲むフラグのオブジェクト
    [SerializeField] GameObject circle1 = default;      
    [SerializeField] GameObject circle2 = default;
    [SerializeField] GameObject exitRange1 = default;
    [SerializeField] GameObject exitRange2 = default;

    GameObject object1, object2;
    string objectName;
    string[] nameSplit = new string[2];
    bool checkNum;
    int num1 = 0, num2 = 0, num3 = 0;
    bool roundFlag, nullFlag, exitFlag = true;

    Vector3 positionA, positionB, positionC, objectPosition;
    float radius1 = 0, radius2 = 0, point1 = 0, point2 = 0;

    public TempCooldownScript CooldownScript;
    public DetectInsideScript DetectInside;

    public static bool END; // 仮に作ります
    bool toUpdate;

    // 判定②用
    int numTemp, numSum;
    public static int midnum;
    public static bool tailChase;


    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 8の字の判定を消す

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            circle1.SetActive(false);
            circle2.SetActive(false);
            exitRange1.SetActive(false);
            exitRange2.SetActive(false);
            roundFlag = false;
            END = false;

            InsideTriggerScript.circle1 = false;
            InsideTriggerScript.circle2 = false;

            tailChase = false;

            // UI用
            ScreenUIScript.circle1stat = false;
            ScreenUIScript.circle2stat = false;

            // バグと調整用
            //circle1.transform.position += new Vector3(0, 0, 10);
            //circle2.transform.position += new Vector3(0, 0, 10);
            //DetectInside.BloomHalt();
            Circle2AdjustmentScript.passNum1 = 0;
            Circle2AdjustmentScript.passNum2 = 0;
        }

        if(toUpdate)
        {
            if (nullFlag == false)
            {
                // 円の判定

                positionA = object1.transform.position;
                if (roundFlag)
                {
                    positionB = objectPosition;
                }
                else
                {
                    positionB = object2.transform.position;
                }


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


                if (roundFlag == false)
                {
                    radius1 = (positionC.x * positionC.x) + (positionC.z * positionC.z);
                    radius1 = Mathf.Sqrt(radius1);
                }
                else if (TempCooldownScript.cooldown == false && roundFlag)
                {
                    radius2 = (positionC.x * positionC.x) + (positionC.z * positionC.z);
                    radius2 = Mathf.Sqrt(radius2);
                }


                // Point of Circle                          円の transform.position を求める
                if (roundFlag == false)
                {
                    if (object1.transform.position.x < object2.transform.position.x)
                    {
                        point1 = object1.transform.position.x;
                    }
                    else
                    {
                        point1 = object2.transform.position.x;
                    }

                    if (object1.transform.position.z < object2.transform.position.z)
                    {
                        point2 = object1.transform.position.z;
                    }
                    else
                    {
                        point2 = object2.transform.position.z;
                    }
                }
                else
                {
                    if (object1.transform.position.x < objectPosition.x)
                    {
                        point1 = object1.transform.position.x;
                    }
                    else
                    {
                        point1 = objectPosition.x;
                    }

                    if (object1.transform.position.z < objectPosition.z)
                    {
                        point2 = object1.transform.position.z;
                    }
                    else
                    {
                        point2 = objectPosition.z;
                    }
                }

                point1 += positionC.x / 2;
                point2 += positionC.z / 2;


                // Circle Transform

                if (roundFlag == false)
                {
                    circle1.SetActive(true);
                    circle1.transform.localScale = new Vector3(radius1, 1, radius1);
                    circle1.transform.position = new Vector3(point1, 1, point2);
                    roundFlag = true;
                    exitFlag = false;
                    CooldownScript.StartCooldown();

                    // UI用
                    ScreenUIScript.circle1stat = true;
                }
                else if (TempCooldownScript.cooldown == false && roundFlag)
                {
                    circle2.SetActive(true);
                    circle2.transform.localScale = new Vector3(radius2, 1, radius2);
                    circle2.transform.position = new Vector3(point1, 1, point2);
                    //roundFlag = false;                                                        // OnTriggerEnter BUG
                    exitFlag = false;


                    END = true; // 仮に作ります

                    // UI用
                    ScreenUIScript.circle2stat = true;
                }

                toUpdate = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CircleFlag" && exitFlag　&& END == false)
        {
            // オブジェクトを読み込む
            objectName = other.name;
            nameSplit = objectName.Split('_');

            if (roundFlag == false)
            {
                checkNum = int.TryParse(nameSplit[1], out num1);
                num2 = num1 / 2;
                object1 = GameObject.Find("Cube_" + num1);  
                object2 = GameObject.Find("Cube_" + num2);
                objectPosition = object1.transform.position;
                
                if (object1 == null || object2 == null || object1.name == "Cube_1" /* 最初のナカマ */)
                {
                    nullFlag = true;
                }
                else
                {
                    exitRange1.SetActive(true);
                    exitRange1.transform.position = object1.transform.position;
                    nullFlag = false;

                    toUpdate = true;
                }
            }
            else if(TempCooldownScript.cooldown == false && roundFlag)
            {
                if (tailChase)
                {
                    Debug.Log("TAIL");
                    numTemp = Circle2AdjustmentScript.passNum1 / 2;
                    object1 = GameObject.Find("Cube_" + numTemp);
                    tailChase = false;
                }
                else
                {
                    checkNum = int.TryParse(nameSplit[1], out num3);

                    // 判定② 調整前
                    //object1 = GameObject.Find("Cube_" + num3);
                    // ここまで

                    //object2 = GameObject.Find("Cube_" + num1);


                    // 判定② 調整後 バグあり
                    midnum = num1;
                    numTemp = num3 - Circle2AdjustmentScript.passNum2;
                    //if(numTemp < 0) // BUG TEST
                    //{
                    //    numTemp *= -1;
                    //}
                    numSum = (numTemp + Circle2AdjustmentScript.passNum1) / 2;

                    if (numTemp <= 0)
                    {
                        Debug.Log("X");
                        Debug.Log("num3 = " + num3);
                        Debug.Log("pass1 = " + Circle2AdjustmentScript.passNum1 + " pass2 = " + Circle2AdjustmentScript.passNum2);
                        Debug.Log("numTemp = " + numTemp + ", numSum = " + numSum);
                    }
                    else if (numTemp > Circle2AdjustmentScript.passNum1)
                    {
                        Debug.Log("A");
                        Debug.Log("num3 = " + num3);
                        num3 = Circle2AdjustmentScript.passNum2 + numSum;
                        Debug.Log("pass1 = " + Circle2AdjustmentScript.passNum1 + " pass2 = " + Circle2AdjustmentScript.passNum2);
                        Debug.Log("numTemp = " + numTemp + ", numSum = " + numSum);
                    }
                    else if (numTemp < Circle2AdjustmentScript.passNum1)
                    {
                        Debug.Log("B");
                        Debug.Log("num3 = " + num3);
                        num3 = Circle2AdjustmentScript.passNum1 - numSum;
                        Debug.Log("pass1 = " + Circle2AdjustmentScript.passNum1 + " pass2 = " + Circle2AdjustmentScript.passNum2);
                        Debug.Log("numTemp = " + numTemp + ", numSum = " + numSum);
                    }

                    object1 = GameObject.Find("Cube_" + num3);
                    // ここまで
                }

                if (object1 == null/* || object2 == null*/)
                {
                    nullFlag = true;
                }
                else
                {
                    nullFlag = false;
                    exitRange2.SetActive(true);
                    exitRange2.transform.position = object1.transform.position;

                    toUpdate = true;
                }
            }

            //if (nullFlag == false)
            //{
            //    // 円の判定

            //    positionA = object1.transform.position;
            //    if(roundFlag)
            //    {
            //        positionB = objectPosition;
            //    }
            //    else
            //    {
            //        positionB = object2.transform.position;
            //    }


            //    // Radius of Circle                         円の半径を求める

            //    positionC = positionA - positionB;
            //    if (positionC.x < 0)
            //    {
            //        positionC.x *= -1;
            //    }
            //    if (positionC.y < 0)
            //    {
            //        positionC.y *= -1;
            //    }
            //    if (positionC.z < 0)
            //    {
            //        positionC.z *= -1;
            //    }


            //    if (roundFlag == false)
            //    {
            //        radius1 = (positionC.x * positionC.x) + (positionC.z * positionC.z);
            //        radius1 = Mathf.Sqrt(radius1);
            //    }
            //    else if (TempCooldownScript.cooldown == false && roundFlag)
            //    {
            //        radius2 = (positionC.x * positionC.x) + (positionC.z * positionC.z);
            //        radius2 = Mathf.Sqrt(radius2);
            //    }


            //    // Point of Circle                          円の transform.position を求める
            //    if (roundFlag == false)
            //    {
            //        if (object1.transform.position.x < object2.transform.position.x)
            //        {
            //            point1 = object1.transform.position.x;
            //        }
            //        else
            //        {
            //            point1 = object2.transform.position.x;
            //        }

            //        if (object1.transform.position.z < object2.transform.position.z)
            //        {
            //            point2 = object1.transform.position.z;
            //        }
            //        else
            //        {
            //            point2 = object2.transform.position.z;
            //        }
            //    }
            //    else
            //    {
            //        if (object1.transform.position.x < objectPosition.x)
            //        {
            //            point1 = object1.transform.position.x;
            //        }
            //        else
            //        {
            //            point1 = objectPosition.x;
            //        }

            //        if (object1.transform.position.z < objectPosition.z)
            //        {
            //            point2 = object1.transform.position.z;
            //        }
            //        else
            //        {
            //            point2 = objectPosition.z;
            //        }
            //    }

            //    point1 += positionC.x / 2;
            //    point2 += positionC.z / 2;


            //    // Circle Transform

            //    if (roundFlag == false)
            //    {
            //        circle1.SetActive(true);
            //        circle1.transform.localScale = new Vector3(radius1, 1, radius1);
            //        circle1.transform.position = new Vector3(point1, 1, point2);
            //        roundFlag = true;
            //        exitFlag = false;
            //        CooldownScript.StartCooldown();

            //        // UI用
            //        ScreenUIScript.circle1stat = true;
            //    }
            //    else if (TempCooldownScript.cooldown == false && roundFlag)
            //    {
            //        circle2.SetActive(true);
            //        circle2.transform.localScale = new Vector3(radius2, 1, radius2);
            //        circle2.transform.position = new Vector3(point1, 1, point2);
            //        //roundFlag = false;                                                        // OnTriggerEnter BUG
            //        exitFlag = false;


            //        END = true; // 仮に作ります

            //        // UI用
            //        ScreenUIScript.circle2stat = true;
            //    }
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CircleFlag")
        {
            exitFlag = true;
        }
    }

    public void DisbandFormation1()
    {
        circle1.SetActive(false);
        exitRange1.SetActive(false);

        roundFlag = false;
        END = false;

        circle2.SetActive(false);
        exitRange2.SetActive(false);

        InsideTriggerScript.circle1 = false;
        InsideTriggerScript.circle2 = false;

        tailChase = false;

        // UI用
        ScreenUIScript.circle1stat = false;
        ScreenUIScript.circle2stat = false;

        // バグと調整用
        //circle1.transform.position += new Vector3(0, 0, 10);
        //circle2.transform.position += new Vector3(0, 0, 10);
        //DetectInside.BloomHalt();

        Circle2AdjustmentScript.passNum1 = 0;
        Circle2AdjustmentScript.passNum2 = 0;
    }

    //public void DisbandFormation2()
    //{
    //    circle2.SetActive(false);
    //    exitRange2.SetActive(false);

    //    roundFlag = false;
    //    END = false;

    //    circle1.SetActive(false);
    //    exitRange1.SetActive(false);
    //    DetectInside.BloomHalt();
        
    //}
}
