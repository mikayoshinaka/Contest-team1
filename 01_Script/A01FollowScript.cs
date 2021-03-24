using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A01FollowScript : MonoBehaviour
{
    public GameObject TrackingObject;
    A02PositionUpdate PositionManager;
    int nextIndex;

    public float speed = 4.5F;
    private float startTime = 0;
    private float journeyLength;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public float waitTime = 0.1f;   // sec
    private float range = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        PositionManager = TrackingObject.GetComponent<A02PositionUpdate>();

        startPosition = transform.position;
        endPosition = PositionManager.GetCurrentPositon(out nextIndex);

        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, endPosition) < range)
        {
            startTime = Time.time;

            startPosition = transform.position;
            endPosition = PositionManager.GetPositon(nextIndex, out nextIndex);
            endPosition.y = startPosition.y;    // Y座標を合わせる

            journeyLength = Vector3.Distance(startPosition, endPosition);
        }

        float distCovered = (Time.time - startTime) * speed;

        float fractionOfJourney = distCovered / journeyLength;

        if (0 < journeyLength)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        }
    }
}
