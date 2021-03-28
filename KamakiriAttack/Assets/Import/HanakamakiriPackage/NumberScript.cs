using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberScript : MonoBehaviour
{
    /*エリア全体にtriggerをしている。enemyと一番近いものを見分ける*/

    [SerializeField] HanakamakiriScript hanakamakiriScript;
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
        if (searchObject.Count == 0)
        {
            hanakamakiriScript.TargetNull();
        }
    }
    public void areafellows(Transform targetObj = null)
    {
        if (searchObject.Count > 0) {
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
            hanakamakiriScript.SetNumber(searchObject[number].transform);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Fellow")
        {

            if (!searchObject.Contains(other.gameObject))
            {
                searchObject.Add(other.gameObject);
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
}
