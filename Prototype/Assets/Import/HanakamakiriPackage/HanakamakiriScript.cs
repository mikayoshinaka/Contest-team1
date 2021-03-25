using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class HanakamakiriScript : MonoBehaviour
{
    /*ハナカマキリ本体*/
    /*navmeshで動かしている。*/
    /*centerはRandom状態の時、enemyが離れるとcenterのほうを向くようになっている。*/
    public enum State
    {
        Random,
        Chase,
        Eater,
        Eating,
        Harmless
    };
    [SerializeField] GameObject hand;
    [SerializeField] InstantDeadScript instantDeadScript;
    [SerializeField] RangeScript rangeScript;
    [SerializeField] GameObject eatArea;
    [SerializeField] AttackRangeScript attackRangeScript;
    [SerializeField] NumberScript numberScript;
    public Transform center;
    private NavMeshAgent agent;
    private Transform target;
    private Transform dead;
    private State state;
    private float harmlessTime = 0.0f;
    private float eatTime = 0.0f;
    private float actionTime=0.0f;
    private float attackTime = 0.0f;
    private float turnTime = 0.1f;
    private float turnRange = 180.0f;
    public float rotatespeed = 30.0f;
    private bool instantAngle=false;
    private float nowRot;
    private Quaternion q;
    private bool attacked = false;
    private bool attack = false;
    private bool attackphase = false;
    private bool deadFull = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetState(State.Random);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 Cpos = center.transform.position;
        Vector3 Epos = this.transform.position;
        float dis = Vector3.Distance(Cpos, Epos);
        Vector3 dir = Cpos - Epos;
        float angle = Vector3.SignedAngle(agent.transform.forward, dir,Vector3.up);
       
       
        if (state == State.Random)
        {
            hand.SetActive(false);
            eatArea.SetActive(false);
           
            
                actionTime += Time.deltaTime;
                if (actionTime >= 0.0f && actionTime < 2.0f)
                {
                    agent.isStopped = true;
                    agent.transform.position += transform.forward * Time.deltaTime;
                }
                else if (actionTime >= 2.0f && actionTime <= 2.5f)
                {
                    if (dis >= 5.0f)
                    {
                        if (instantAngle == false)
                        {

                            nowRot = agent.transform.rotation.eulerAngles.y;
                            nowRot += angle;
                            instantAngle = true;
                        }
                        q = Quaternion.AngleAxis(nowRot, Vector3.up);
                        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, q, Time.deltaTime / turnTime);

                    }
                    else
                    {
                        if (instantAngle == false)
                        {

                            nowRot = agent.transform.rotation.eulerAngles.y;
                            nowRot += Random.Range(-turnRange, turnRange);
                            instantAngle = true;
                        }
                        q = Quaternion.AngleAxis(nowRot, Vector3.up);
                        agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, q, Time.deltaTime / turnTime);
                    }
                }
                else
                {
                    instantAngle = false;
                    actionTime = 0.0f;
                }
            
        }
        else if(state==State.Harmless)
        {
            eatArea.SetActive(false);
            agent.isStopped = true;
            harmlessTime += Time.deltaTime;
            attackRangeScript.Eating();
            rangeScript.Eating();
            if (harmlessTime>=3.0f) {
                harmlessTime = 0.0f;
                attackRangeScript.Ate();
                rangeScript.Ate();
                rangeScript.Harm();
                SetState(State.Random);
            }
        }
        else if(state==State.Chase)
        {
            rangeScript.Harmless();
            actionTime = 0.0f;
            if (target == null)
            {
                SetState(State.Random);
            }
            numberScript.areafellows(this.transform);
            
            if (attacked==false) {
                agent.isStopped = false;
                if (!(target == null))
                {
                    agent.destination = target.position;
                }
            }
            else
            {
                agent.isStopped = true;
                attackTime += Time.deltaTime;
                if (attackTime>=2.5f)
                {
                    attacked = false;
                    attackTime = 0.0f;
                    if (deadFull == true)
                    {
                        
                        SetState(State.Eater);
                    }
                }
            }
            if (attack==true&&attackphase==false)
            {
                attackphase = true;
                hand.SetActive(true);
            }
            if (attackphase == true)
            {
                attackTime += Time.deltaTime;
                if (attackTime >= 2.0f)
                {
                    attack = false;
                    attackphase = false;
                    hand.SetActive(false);
                    attacked = true;
                }
            }
           
        }
        else if(state == State.Eater)
        {
            hand.SetActive(false);
            rangeScript.Harm();
            attackRangeScript.Eating();
            rangeScript.Eating();
            eatArea.SetActive(true);
            instantDeadScript.DeadSearch(this.transform);
            agent.isStopped = false;
            if (!(dead == null)) {
                agent.destination = dead.position;
            }
            if (dead==null)
            {
                rangeScript.NotEatCount();
                attackRangeScript.Ate();
                rangeScript.Ate();
                deadFull = false;
                eatArea.SetActive(false);
                SetState(State.Random);
            }
        }
        else if(state==State.Eating)
        {
            eatArea.SetActive(false);
            agent.isStopped = true;
            eatTime += Time.deltaTime;
            if(eatTime>=2.0f)
            {
                attackRangeScript.Ate();
                rangeScript.Ate();
                instantDeadScript.DeadEnd();
                rangeScript.NotEatCount();
                deadFull = false;
                eatTime = 0.0f;
                SetState(State.Random);
            }
        }
       
    }

    public void SetState(State temp)
    {
        if(temp==State.Random)
        {
            state = temp;
        }
        else if (temp == State.Chase)
        {
            state = temp;
        }
        else if(temp == State.Eater)
        {
            state = temp;
        }
        else if(temp == State.Eating)
        {
            state = temp;
        }
        else if(temp==State.Harmless)
        {
            state = temp;
        }
    }
    public void SetNumber(Transform targetObj = null)
    {
        target = targetObj;
    }
    public void SetDead(Transform targetObj = null)
    {
        dead = targetObj;
    }
   
    public State GetState()
    {
        return state;
    }

    public void OnAttack()
    {
        attack = true;
    }

    public void Attacked()
    {
        attacked = true;
    }

    public void DeadFull()
    {
        deadFull = true;
    }

    public void DeadNull()
    {
        dead = null;
    }
   
    public void TargetNull()
    {
        target = null;
    }
}
