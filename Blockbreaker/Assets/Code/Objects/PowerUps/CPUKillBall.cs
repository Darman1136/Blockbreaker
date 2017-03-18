using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUKillBall : CPowerUp {
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

    public override CSerializableSpawnableObject GetSerializableObject() {
        CSerializableSpawnableObject sso = base.GetSerializableObject();
        sso.data.Add("type", Type.CPUKillBall);
        return sso;
    }
}
