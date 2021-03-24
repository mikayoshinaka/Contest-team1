using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUIScript : MonoBehaviour
{
    [SerializeField] GameObject nakamaText;
    [SerializeField] GameObject nakamaBar;

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
    }
}
