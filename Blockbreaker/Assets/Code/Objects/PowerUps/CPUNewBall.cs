using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUNewBall : CPowerUp
{
    private CPlayerInfo pi;
    public override void Start()
    {
        base.Start();
        GameObject goInformation = GameObject.Find("Information");
        pi = goInformation.GetComponent<CPlayerInfo>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("PlayerBall"))
        {
            pi.Balls = ++pi.Balls;
            Destroy(this.gameObject);
        }
    }
}
