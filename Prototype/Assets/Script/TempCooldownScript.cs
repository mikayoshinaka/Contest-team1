using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCooldownScript : MonoBehaviour
{
    //  public CameraScript myCameraScript;    
    //  myCameraScript.ResetRotation();
    public static bool cooldown;
    private float timer = 0;
    private float timeLimit;

    // Start is called before the first frame update
    void Start()
    {
        timeLimit = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown)
        {
            timer += Time.deltaTime;
            if(timer >= timeLimit)
            {
                cooldown = false;
            }
        }
    }

    public void StartCooldown()
    {
        cooldown = true;
        timer = 0;
    }
}
