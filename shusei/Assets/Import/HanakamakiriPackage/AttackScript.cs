using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    /*攻撃判定
     * タグ変更で攻撃をしている
     また、死んだものをinstantdeadに送っている*/
    [SerializeField] InstantDeadScript instantDeadScript;
   
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
       
            if (other.tag == "Player" || other.tag == "Fellow")
            {
                instantDeadScript.DeadPlus(other.transform);
                other.gameObject.tag = "Death";
            }
        
    }

}
