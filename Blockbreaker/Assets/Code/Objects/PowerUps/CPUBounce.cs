using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBounce : CPowerUp {

    public override void Start()
    {
        base.Start();
        Debug.Log("Start of bounce");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("PlayerBall"))
        {
            CBall ball = collision.gameObject.GetComponent<CBall>();
            ball.Bounce = true;
        }
    }

}
