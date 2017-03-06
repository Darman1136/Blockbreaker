using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlock : MonoBehaviour {
    private SpriteRenderer sr;
    private CBlockHealthText cbht;
    private CDefaultGamemode gamemode;

    private int health = 99999;
    public int Health {
        set {
            health = value;
            SetColor();
        }
        get {
            return health;
        }
    }

    void Start() {
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
        sr = GetComponent<SpriteRenderer>();
        cbht = GetComponentInChildren<CBlockHealthText>();
        cbht.SetInitialText(Health.ToString());
        SetColor();
    }

    void Update() {
        if (!IsAlive()) {
            Destroy(gameObject);
        }
    }

    private void SetColor() {
        if (sr != null) {
            if (Health < 16) {
                sr.color = Color.white;
            } else if (Health < 32) {
                sr.color = Color.red;
            } else if (Health < 64) {
                sr.color = Color.yellow;
            } else if (Health < 128) {
                sr.color = Color.green;
            } else if (Health < 256) {
                sr.color = Color.magenta;
            } else if (Health < 512) {
                sr.color = Color.blue;
            }
        }
    }

    private bool IsAlive() {
        return Health > 0;
    }

    void Hit(int amt) {
        gamemode.addPoints(amt);
        Health = Health - amt;
        cbht.UpdateText(Health.ToString());
    }
}
