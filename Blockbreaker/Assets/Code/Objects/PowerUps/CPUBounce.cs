using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBounce : CPowerUp {

    public override void Start() {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerBall")) {
            CBall ball = collision.gameObject.GetComponent<CBall>();
            ball.Bounce = true;
            ball.GetComponent<SpriteRenderer>().color = Color.cyan;
            DestoryAtEndOfRound = true;
        }
    }

    public override CSerializableSpawnableObject GetSerializableObject() {
        CSerializableSpawnableObject sso = base.GetSerializableObject();
        sso.data.Add("type", Type.CPUBounce);
        return sso;
    }
}
