using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A01FollowScript : MonoBehaviour
{
    public GameObject TrackingObject;
    A02PositionUpdate PositionManager;
    int nextIndex;

    public float speed = 4.5F;
    private float startTime = 0;
    private float journeyLength;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public float waitTime = 0.1f;   // sec
    private float range = 0.001f;



    //追跡判定の変更処理。

    //forwardA01は「自身の前方に位置するオブジェクトにアタッチされたスクリプト」を示す。
    public A01FollowScript forwardA01;
    public bool friendState = true;

    //当たり判定を持ってくれるオブジェクトの参照。
    GameObject colObject;


    // Start is called before the first frame update
    void Start()
    {
        PositionManager = TrackingObject.GetComponent<A02PositionUpdate>();

        startPosition = transform.position;
        endPosition = PositionManager.GetCurrentPositon(out nextIndex);

        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);

        forwardA01 = TrackingObject.GetComponent<A01FollowScript>();

        //子オブジェクトのタグを引っ張ってくるために必要な処理。
        
        /*colObject = transform.GetChild(0).gameObject;*/

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, endPosition) < range)
        {
            startTime = Time.time;

            startPosition = transform.position;
            endPosition = PositionManager.GetPositon(nextIndex, out nextIndex);
            endPosition.y = startPosition.y;    // Y座標を合わせる

            journeyLength = Vector3.Distance(startPosition, endPosition);
        }

        //Lerpによる移動。詳しくは口頭で説明します。

        float distCovered = (Time.time - startTime) * speed;

        float fractionOfJourney = distCovered / journeyLength;

        if (0 < journeyLength)
        {
            if (friendState == true)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            }
        }

        //攻撃を食らった場合
        //このScriptをアタッチしているオブジェクトが消える。
        //このScriptから見て、前方と後方を接続する必要がある。
        //TrackingObjectと、このオブジェクトをTrackingObjectとしているオブジェクトの接続。
        //TrackingObjectに張り付いたFollowScriptの変数を参照。Deathになった場合にTrackingObjectから次の参照先を引っ張ってくる。
        //常に前方objの状態をチェックする。


        //friendState は仲間の状態。あくまで仮置きの為、boolで管理。死亡状態をfalseとしている。
        if (forwardA01.friendState == false)
        {
            //forwardA01によって、「前の仲間が」追跡しているオブジェクトを参照し、自身の追跡先として再定義する。

            TrackingObject = forwardA01.TrackingObject;
            forwardA01 = TrackingObject.GetComponent<A01FollowScript>();

            PositionManager = TrackingObject.GetComponent<A02PositionUpdate>();

            //座標の更新
            startTime = Time.time;

            startPosition = transform.position;
            endPosition = PositionManager.GetPositon(nextIndex, out nextIndex);
            endPosition.y = startPosition.y;    // Y座標を合わせる

            journeyLength = Vector3.Distance(startPosition, endPosition);

        }

        //死亡判定が子オブジェクトのコライダーで行っているらしい。そこは要修正。
        //colObject.tag == death
        if (tag == "Death")
        {
            friendState = false;
            //仲間の消滅処理
            //gameObject.SetActive(false);
        }
    }
}
