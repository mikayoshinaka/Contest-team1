using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class RangeScript : MonoBehaviour
{
    /*索敵範囲*/
    /*範囲内に入ったら追いかけるようにする*/
    [SerializeField]  HanakamakiriScript hanakamakiriScript;
    [SerializeField] InstantDeadScript instantDeadScript;
    [SerializeField]  SphereCollider search;
    [SerializeField]  float searchAngle = 70f;
    [SerializeField] float verticalSearchAngle = 30f;
    private List<GameObject> searchObject = new List<GameObject>();
    private float second = 5.0f;
    private bool exit = true;
    private bool eat = false;
    private bool eatCount = false;
    private bool harmLess = false;
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        if (eat == false)
        {
            if (searchObject.Count == 0)
            {

                exit = true;
            }
            if (searchObject.Count >= 1)
            {
                hanakamakiriScript.SetState(HanakamakiriScript.State.Chase);
            }

            if (exit && second < 5.0f)
            {
                second += Time.deltaTime;
            }
            if (second >= 5.0f)
            {
                instantDeadScript.EatOr();
                if (eatCount==false && harmLess==true) {
                    hanakamakiriScript.SetState(HanakamakiriScript.State.Harmless);
                }
                else if(eatCount == false && harmLess == false)
                {
                    hanakamakiriScript.SetState(HanakamakiriScript.State.Random);
                }
                else
                {
                    hanakamakiriScript.SetState(HanakamakiriScript.State.Eater);
                }
            }
           
        }
       
    }
   public void Harmless()
    {
        harmLess = true;
    }
   public void Harm()
    {
        harmLess = false;
    }
    public void Eating()
    {
        eat = true;
    }
   
    public void Ate()
    {
        second = 5.0f;
        eat = false;
    }
    public void NotEatCount()
    {
        eatCount = false;
    }

    public void EatCount()
    {
        eatCount = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"||other.tag=="Fellow")
        {
           
            Vector3 playerDirection = other.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, playerDirection);
            Vector3 otherUnder = new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z);
            Vector3 otherDirection = otherUnder - transform.position;
            float verticalangle = Vector3.Angle(otherDirection, playerDirection);
            if (angle <= searchAngle&&verticalangle<=verticalSearchAngle)
            {
                
                if (!searchObject.Contains(other.gameObject))
                {
                    searchObject.Add(other.gameObject);
                }
                exit = false;
                second = 0.0f;
            }
            else
            {
                if (searchObject.Contains(other.gameObject))
                {
                    searchObject.Remove(other.gameObject);
                }
            }

        }
        else
        {
            if (searchObject.Contains(other.gameObject))
            {
                searchObject.Remove(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Fellow")
        {

            if (searchObject.Contains(other.gameObject))
            {
                searchObject.Remove(other.gameObject);
            }
        }
    }


}
