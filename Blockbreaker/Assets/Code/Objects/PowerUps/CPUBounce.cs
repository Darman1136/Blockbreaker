using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBounce : CPowerUp {

    public override void Start() {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag.Equals("PlayerBall")) {
            CBall ball = collision.gameObject.GetComponent<CBall>();
            ball.Bounce = true;
            ball.GetComponent<SpriteRenderer>().color = Color.cyan;
            DestoryAtEndOfRound = true;
        }
    }

}
