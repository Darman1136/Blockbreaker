using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUAdvancedAimLine : CPowerUp {
    private CDefaultGamemode gamemode;
    public override void Start() {
        base.Start();
        gamemode = CDefaultGamemode.GAMEMODE;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag.Equals("PlayerBall")) {
            gamemode.SetAdvancedAimLine();
            Destroy(this.gameObject);
        }
    }

    public override CSerializableSpawnableObject GetSerializableObject() {
        CSerializableSpawnableObject sso = base.GetSerializableObject();
        sso.data.Add("type", Type.CPUAdvancedAimLine);
        return sso;
    }
}
