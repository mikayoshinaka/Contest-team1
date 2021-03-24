using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class AttackRangeScript : MonoBehaviour
{
    /*攻撃範囲のスクリプト*/
    /*判定内に入ったら攻撃判定を呼ぶ*/
    [SerializeField] SphereCollider search;
    [SerializeField] float searchAngle = 70f;
    [SerializeField] GameObject hanakamakiri;
    [SerializeField] float verticalSearchAngle = 30f;
    private bool eat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (eat == false)
        {
            if (other.tag == "Player" || other.tag == "Fellow")
            {
                Vector3 playerDirection = other.transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, playerDirection);
                Vector3 otherUnder = new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z);
                Vector3 otherDirection = otherUnder - transform.position;
                float verticalangle = Vector3.Angle(otherDirection, playerDirection);
                if (angle <= searchAngle&&verticalangle<=verticalSearchAngle)
                {
                    hanakamakiri.GetComponent<HanakamakiriScript>().OnAttack();
                }
            }
        }
    }
    public void Eating()
    {
        eat = true;

    }

    public void Ate()
    {
        eat = false;
    }


}
