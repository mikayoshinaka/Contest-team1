using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A02PositionUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3[] position = new Vector3[100];
    int setCounter = 0;

    float waitTime = 1.5f;   // 記録の間隔　sec
    private float timer = 0.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            timer = 0;

            position[setCounter] = transform.position;
            setCounter++;


            if (position.Length <= setCounter)
            {
                setCounter = 0;
            }
        }
    }

    public Vector3 GetCurrentPositon(out int nextIndex)
    {
        nextIndex = setCounter;

        return transform.position;
    }

    public Vector3 GetPositon(int index, out int nextIndex)
    {
        Vector3 retPostion;

        //Debug.Log(index);

        retPostion = position[index];

        // 追随する仲間が追い付いた場合
        if (setCounter == index)
        {
            nextIndex = setCounter;

            // position[index]の情報は更新されていないので１個前のデータを渡す
            if (0 == nextIndex)
            {
                retPostion = position[position.Length - 1];
            }
            else
            {
                retPostion = position[index - 1];
            }
        }
        else
        {
            nextIndex = index + 1;

            if (position.Length <= nextIndex)
            {
                nextIndex = 0;
            }
        }

        return retPostion;
    }
}
