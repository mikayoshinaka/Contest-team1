using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fellowshipSpawn : MonoBehaviour
{
   
    GameObject object1, object2, object3, object4, followobject1, followobject2, followobject3, followobject4;
    float distance1, distance2, distance3, distance4, StopRange = 5f,spawnTime;
    Vector3 direction1, direction2, direction3, direction4;
    RaycastHit RH;
    int num = 50,newNum;
    //判定サークルのオブジェクト取得
    [SerializeField] GameObject circle1 = default;
    [SerializeField] GameObject circle2 = default;

    //プレイヤー
    [SerializeField] GameObject player = default;
    //味方
    [SerializeField] GameObject friendParent = default;
   
    
    //リスポーン時使用変数
    bool follow1, follow2, follow3, follow4, limit,start=false;
    public static bool startSpawn;
    //リスポーン対象:花(0):キノコ(1):木(2)
    //public static int spawnKind=1;

    //メモ
    //まずどのオブジェクトか取得し、アニメーションを開始させる。
    //tag判定でアニメーション判別

    Animator animator;
    AnimatorStateInfo stateInfo;
    void Start()
    {
        startSpawn = true;
        
    }

    void Update()
    {
        if (!start)
        {
            limit = false;
        }

        //円の判定（仮）
        if ((circle1.transform.position.x - 2.0f <= this.transform.position.x && circle1.transform.position.x + 2.0f >= this.transform.position.x &&
            circle1.transform.position.z - 2.0f <= this.transform.position.z && circle1.transform.position.z + 2.0f >= this.transform.position.z)||
            (circle2.transform.position.x - 2.0f <= this.transform.position.x && circle2.transform.position.x + 2.0f >= this.transform.position.x &&
            circle2.transform.position.z - 2.0f <= this.transform.position.z && circle2.transform.position.z + 2.0f >= this.transform.position.z) && SurroundScript.END)
            start = true;

            //Spawn=trueでリスポーン処理開始
            if (start)
        {

            if (this.gameObject.CompareTag("flower"))
            {

                //スポーン座標を取得
                Vector3 spawnPos;
                spawnPos = transform.position;
                //対象のアニメーションを取得
                animator=this.GetComponent<Animator>();
                
               

                if (!limit) {
                    //花の再生開始
                    animator.SetBool("bloomMotion", true);
                    follow1 = true;
                    follow2 = true;
                    follow3 = true;
                    follow4 = true;
                    spawnTime = 2.0f;
                    limit = true;
                    newNum = TemporaryFriendScript.friendCount;
                }
                spawnTime -= Time.deltaTime;
                //スポーン処理
                if (TemporaryFriendScript.friendCount == newNum&&spawnTime<2.0f)
                {   //1
                    object1 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    followobject1 = friendParent.transform.Find("Cube (" + (TemporaryFriendScript.friendCount - 1) + ')').gameObject;
                    object1.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object1.transform.position = this.transform.position + new Vector3(0, 2, 0);
                }
                if (TemporaryFriendScript.friendCount == newNum+1 && spawnTime < 1.8f)
                {   //2
                    object2 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    object2.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object2.transform.position = this.transform.position + new Vector3(0, 2, 0);
                }
                if (TemporaryFriendScript.friendCount == newNum + 2 && spawnTime < 1.6f)
                {   //3
                    object3 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    object3.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object3.transform.position = this.transform.position + new Vector3(0, 1, 2);
                }
                

            }
            if (this.gameObject.CompareTag("mushroom"))
            {
                //スポーン座標を取得
                Vector3 spawnPos;
                spawnPos = transform.position;
                //対象のアニメーションを取得
                animator = this.GetComponent<Animator>();
               
                if (!limit)
                {
                    //キノコの再生
                    animator.SetBool("mushroomMotion", true);
                    follow1 = true;
                    follow2 = true;
                    follow3 = true;
                    follow4 = true;
                    spawnTime = 2.0f;
                    limit = true;
                    newNum = TemporaryFriendScript.friendCount;
                }
                spawnTime -= Time.deltaTime;
                //スポーン処理
                if (TemporaryFriendScript.friendCount == newNum && spawnTime < 2.0f)
                {   //1
                    object1 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    followobject1 = friendParent.transform.Find("Cube (" + (TemporaryFriendScript.friendCount - 1) + ')').gameObject;
                    object1.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object1.transform.position = this.transform.position + new Vector3(0, 2, 0);
                }
                if (TemporaryFriendScript.friendCount == newNum + 1 && spawnTime < 1.8f)
                {   //2
                    object2 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    object2.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object2.transform.position = this.transform.position + new Vector3(0, 2, 0);
                }
                if (TemporaryFriendScript.friendCount == newNum + 2 && spawnTime < 1.6f)
                {   //3
                    object3 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    object3.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object3.transform.position = this.transform.position + new Vector3(0, 1, 2);
                }
                if (TemporaryFriendScript.friendCount == newNum + 3 && spawnTime < 1.4f)
                {   //4
                    object4 = friendParent.transform.Find("Cube (" + TemporaryFriendScript.friendCount + ')').gameObject;
                    object4.SetActive(true);
                    TemporaryFriendScript.friendCount++;
                    object4.transform.position = this.transform.position + new Vector3(0, 1, 2);
                }
            }
        }
    }
}

//保存用ぼつ

// インスタンスを生成、
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z), Quaternion.identity);
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z), Quaternion.identity);
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z), Quaternion.identity);
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z), Quaternion.identity);
//Spawn = false;

//最初の追従処理
//if (follow1) { 
//    distance1 = Vector3.Distance(object1.transform.position, followobject1.transform.position);
//    if (Physics.Raycast((Camera.main.ScreenPointToRay(Input.mousePosition)), out RH, 100))
//    {
//        object1.transform.position += object1.transform.forward * 0.3f; // 0.1fから
//        if (CubeFollow.tracking)
//        {
//            Debug.Log("dd");
//            follow1 = false;
//        }
//    }
//    direction1 = followobject1.transform.position - object1.transform.position;
//    object1.transform.rotation = Quaternion.LookRotation(new Vector3(direction1.x, 0, direction1.z));
//}
//if (follow2)
//{
//    distance2 = Vector3.Distance(object2.transform.position, player.transform.position);
//    if (Physics.Raycast((Camera.main.ScreenPointToRay(Input.mousePosition)), out RH, 100))
//    {
//        object2.transform.position += object2.transform.forward * 0.3f; // 0.1fから
//        if (CubeFollow.tracking)
//        {
//            follow2 = false;
//        }
//    }
//    direction2 = player.transform.position - object2.transform.position;
//    object2.transform.rotation = Quaternion.LookRotation(new Vector3(direction2.x, 0, direction2.z));
//}
//if (follow3)
//{
//    distance3 = Vector3.Distance(object3.transform.position, player.transform.position);
//    if (Physics.Raycast((Camera.main.ScreenPointToRay(Input.mousePosition)), out RH, 100))
//    {
//        object3.transform.position += object3.transform.forward * 0.3f; // 0.1fから
//        if (CubeFollow.tracking)
//        {
//            follow3 = false;
//        }
//    }
//    direction3 = player.transform.position - object3.transform.position;
//    object3.transform.rotation = Quaternion.LookRotation(new Vector3(direction3.x, 0, direction3.z));
//}
//if (follow4)
//{
//    distance4 = Vector3.Distance(object4.transform.position, player.transform.position);
//    if (Physics.Raycast((Camera.main.ScreenPointToRay(Input.mousePosition)), out RH, 100))
//    {
//        object4.transform.position += object4.transform.forward * 0.3f; // 0.1fから
//        if (CubeFollow.tracking)
//        {
//            follow4 = false;
//        }
//    }
//    direction4 = player.transform.position - object4.transform.position;
//    object4.transform.rotation = Quaternion.LookRotation(new Vector3(direction4.x, 0, direction4.z));
//}
// インスタンスを生成、
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y+3.4f, spawnPos.z), Quaternion.identity);
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z), Quaternion.identity);
//Instantiate(allyObj, new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z), Quaternion.identity);