using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeFollow : MonoBehaviour
{

    public Transform[] points;
    [SerializeField] int destPoint = 0;
    private NavMeshAgent agent;

    Vector3 playerPos;
    [SerializeField] GameObject player;
    float distance;
    Vector3 direction;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 20f;
    float StopRange = 5f;
    [SerializeField] NavMeshAgent backNav;
    public bool tracking = false;

    RaycastHit RH;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = true;

        //追跡したいオブジェクトの名前を入れる
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);
        /*direction = transform.position - player.transform.position;
        direction.y = 0;*/



        if (tracking)
        {
            
            if (Physics.Raycast((Camera.main.ScreenPointToRay(Input.mousePosition)), out RH, 100))
            {
                if (distance > StopRange)
                {

                    transform.position += transform.forward * 0.3f; // 0.1fから
                }
            }
            direction = playerPos - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            /*//方向の取得
            direction = playerPos - transform.position;
            //回転
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //移動
            if (distance > StopRange){

                transform.position += transform.forward * 0.1f;
            }*/

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 0.3f);

            /*if (distance < StopRange)
            {
                //targetに向かって進む
                //transform.position += transform.forward * 3;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 0.3f);
                transform.position += transform.forward * 0.1f;
            }*/




            /*transform.LookAt(direction);
            transform.position = player.transform.position + new Vector3(0,0,3);*/


            //追跡の時、quitRangeより距離が離れたら中止
            /*if (distance > quitRange)
                tracking = false;*/
            /*if (distance < StopRange /*&& backNav.isStopped)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }


            //Playerを目標とする
            agent.destination = playerPos;*/
        }
        else
        {
            //PlayerがtrackingRangeより近づいたら追跡開始
            //if (distance < trackingRange)
                //tracking = true;
        }
    }
}

//めも
//隊列を組ませる必要がある。
//検知する度に配列

//挙動の話
//8の字を描くのが根本的に難しい。
//そもそも、8の字を描くという行為そのものが、停止に距離を用いる事と合致してない。
//→「返し」がある為。距離以外で停止させる方法を見つけるのがいいか？

//マウスによる移動

//任意停止にする方法。
//入力で友達を「設置」。「設置」したオブジェクト間をつなぐ光線でメビウスを描く手法。
//→現状考える中で最も簡単にイメージに近づける方法。

//プレイヤーが通った軌跡をなぞるようなコード。
//→入力通りに動かせばよい。プレイヤーと仲間の隊列を定義。整列コマンドを用意することで、操作を助ける。
//常に一列にならないか？やってみないと不明。
//

//一定時間ごとに目標地点を更新。目標地点は自身の前にいるオブジェクトの位置。
//→今までと変わらん。
//曲がる角度に制限を持たせることで、円の収縮を抑える手法。

//プレイヤーが通った部分だけ地面が盛り上がり、NavMeshの移動可能範囲を盛り上がった部分だけにし、強引にプレイヤーと同じ軌道を描かせる手法。
//距離は連れている友達の数に応じて伸びる。最後尾の後ろは前が進む度に元に戻る。
//簡単に表せば、プレイヤーが通った地点にオブジェクトが生成され続け、仲間は生成されたオブジェクトの上を進むような形。
//生成する足場の高さに合わせて仲間の位置を調整(NavMeshで高さの下限を設定する？)することで、進路からの落下を防ぐ、など。
//

//通った軌跡の話。
//一定期間毎にPointを更新。更新対象をPlayerのPositionに固定。→いや意味ないか。目指す位置が軌跡に沿うだけで経過が軌跡に沿わない、
//そもそもNavMeshを使うか否かの点について。使わない方が丸そう。使うのであれば、上の道を精製する方法くらいだろうか。
//transformを使うのであれば、単純にプレイヤーの入力をそのまま友達達にも適応すれば、軌跡の上以外を進む事はなくなる。
//プレイヤーと友達間の距離をリセットするような処理を追加。一列に並んだ後に全てに入力を適応する。
//→のはダメだと気付いた。一直線に並んだ際、右入力をすると列が全て右にずれるだけになる。
//つまり、初期地点のズレ以上に差が生まれることがなくなる。恐らく仕様の通りには動作しない。
//やはり、一定期間毎にPlayerの位置を参照する手法しかないか？
//移動距離を計測し、移動距離が一定に達するごとに目標座標を更新。NavMeshによるズレは更新距離を短くすることで対応？
//Positionの場合、前座標に瞬間移動するのでダメ。
//Player(一つ前のオブジェクト)の居る方向を見続けて、transform.position += Vector3.forwardで加算する方法。
//記述
//Updateの中身
//  方向の取得
//direction = RH.point - playerPos;
//  回転
//transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
//  移動
//transform.position += transform.forward * speed;

//前方参照
//