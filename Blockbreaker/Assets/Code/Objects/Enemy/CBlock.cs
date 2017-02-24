using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock : MonoBehaviour
{
    private static int MAX_HEALTH = 20;
    public int health = MAX_HEALTH;

    private SpriteRenderer sr;
    private CBlockHealthText cbht;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cbht =  GetComponentInChildren<CBlockHealthText>();
        cbht.SetInitialText(health.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive())
        {
            Destroy(gameObject);
        }
        else
        {
            setColor();
        }
    }

    private void setColor()
    {
        switch(health)
        {
            case 15:
                sr.color = Color.green;
                break;
            case 10:
                sr.color = Color.yellow;
                break;
            case 5:
                sr.color = Color.red;
                break;
            default:            
                break;
        }
    }

    private bool isAlive()
    {
        return health > 0;
    }

    void Hit(int amt)
    {
        health -= amt;
        cbht.UpdateText(health.ToString());
    }
}
