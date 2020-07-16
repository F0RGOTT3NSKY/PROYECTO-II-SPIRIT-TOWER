using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RatMovementScript : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public float startWaitTime;
    public Transform[] moveSpots;
    public int randomSpot;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = UnityEngine.Random.Range(0, moveSpots.Length);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = UnityEngine.Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
