using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimedSpawner : MonoBehaviour
{

    private static float MAX_DELAY = 1;
    private float currentDelay = MAX_DELAY;

    public GameObject objectToSpawn;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timerTick(Time.deltaTime))
        {
            if (objectToSpawn != null)
            {
                GameObject spawnedObject = Instantiate(objectToSpawn, new Vector2(0f, 0.35f), Quaternion.identity);
                spawnedObject.GetComponent<CBall>().SetInitialVelocity(new Vector2(7f,7f));
            }
        }
    }

    private bool timerTick(float passedMs)
    {
        currentDelay -= passedMs;
        if (currentDelay <= 0)
        {
            resetTimer();
            return true;
        }
        return false;
    }

    private void resetTimer()
    {
        currentDelay = MAX_DELAY + currentDelay;
    }
}
