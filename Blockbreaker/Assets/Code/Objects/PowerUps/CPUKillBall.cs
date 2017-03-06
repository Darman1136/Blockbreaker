using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUKillBall : CPowerUp {
    private CDefaultGamemode gamemode;

    public override void Start() {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag.Equals("PlayerBall")) {
            DestoryAtEndOfRound = true;
            CBall ball = coll.gameObject.GetComponent<CBall>();
            ball.KillBall(false);
        }
    }
}
