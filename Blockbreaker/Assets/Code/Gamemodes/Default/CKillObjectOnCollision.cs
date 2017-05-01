using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKillObjectOnCollision : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("PlayerBall")) {
            CBall ball = coll.gameObject.GetComponent<CBall>();
            if (ball.Bounce) {
                ball.Bounce = false;
            } else {
                ball.KillBall(true);
            }
        }
    }
}
