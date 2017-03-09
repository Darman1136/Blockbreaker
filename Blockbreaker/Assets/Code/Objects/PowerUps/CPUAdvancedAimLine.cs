using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUAdvancedAimLine : CPowerUp {
    private CDefaultGamemode gamemode;
    public override void Start() {
        base.Start();
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag.Equals("PlayerBall")) {
            gamemode.SetAdvancedAimLine();
            Destroy(this.gameObject);
        }
    }
}
