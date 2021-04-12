using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUIScript : MonoBehaviour
{
    [SerializeField] GameObject nakamaText;
    [SerializeField] GameObject nakamaBar;

    [SerializeField] GameObject Circle1;
    [SerializeField] GameObject Circle1Status;
    [SerializeField] GameObject Circle1Pos;

    [SerializeField] GameObject Circle2;
    [SerializeField] GameObject Circle2Status;
    [SerializeField] GameObject Circle2Pos;

    public static bool circle1stat, circle2stat;
    int maxFriend;
    // Start is called before the first frame update
    void Start()
    {
        maxFriend = 50;
    }

    // Update is called once per frame
    void Update()
    {
        nakamaText.GetComponent<Text>().text = "" + TemporaryFriendScript.friendCount;
        nakamaBar.GetComponent<Slider>().value = maxFriend - TemporaryFriendScript.friendCount; // ナカマ最大値 - friendCount

        if(circle1stat)
        {
            Circle1Status.GetComponent<Text>().text = "Circle 1 = ON";
            Circle1Pos.GetComponent<Text>().text = "Position = (" + Circle1.transform.position.x + ", " + Circle1.transform.position.y + ", " + Circle1.transform.position.z + ')';
        }
        else if (circle1stat == false)
        {
            Circle1Status.GetComponent<Text>().text = "Circle 1 = OFF";
            Circle1Pos.GetComponent<Text>().text = "Position = (0, 0, 0)";
        }

        if (circle2stat)
        {
            Circle2Status.GetComponent<Text>().text = "Circle 2 = ON";
            Circle2Pos.GetComponent<Text>().text = "Position = (" + Circle2.transform.position.x + ", " + Circle2.transform.position.y + ", " + Circle2.transform.position.z + ')';
        }
        else if (circle2stat == false)
        {
            Circle2Status.GetComponent<Text>().text = "Circle 2 = OFF";
            Circle2Pos.GetComponent<Text>().text = "Position = (0, 0, 0)";
        }
    }
}
