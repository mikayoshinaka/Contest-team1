using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstantDeadScript : MonoBehaviour
{
    /*エリア全体にかける*/
    /*死んだものを記録する捕食の時に一番近いものをenemyに追いかけさせる*/
    [SerializeField] HanakamakiriScript hanakamakiriScript;
    [SerializeField] RangeScript rangeScript;
    private List<GameObject> searchObject = new List<GameObject>();
    private float dis;
    private int number;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (searchObject.Count>=5)
        {
            hanakamakiriScript.DeadFull();
        }
       if(searchObject.Count == 0)
        {
            hanakamakiriScript.DeadNull();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(!(other.tag=="Death"))
        {
            if (searchObject.Contains(other.gameObject))
            {
                searchObject.Remove(other.gameObject);
            }
        }
    }
    public void DeadSearch(Transform targetObj = null)
    {
        if (searchObject.Count > 0)
        {
            for (int i = 0; i < searchObject.Count; i++)
            {
                if (i == 0)
                {
                    dis = Vector3.Distance(searchObject[i].transform.position, targetObj.transform.position);
                    number = 0;
                }
                else
                {
                    if (Vector3.Distance(searchObject[i].transform.position, targetObj.transform.position) < dis)
                    {
                        dis = Vector3.Distance(searchObject[i].transform.position, targetObj.transform.position);
                        number = i;
                    }
                }
            }
            hanakamakiriScript.SetDead(searchObject[number].transform);
        }
    }
    public void DeadPlus(Transform searchObj = null)
    {
        if (!searchObject.Contains(searchObj.gameObject))
        {
            searchObject.Add(searchObj.gameObject);
        }
    }
    public void EatOr()
    {
        if (searchObject.Count == 0)
        {
            rangeScript.NotEatCount();
        }
        else
        {
            rangeScript.EatCount();
        }
    }
    public void DeadEnd()
    {
        searchObject.Clear();
    }
}
