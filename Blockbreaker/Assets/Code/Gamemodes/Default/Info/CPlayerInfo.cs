using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CPlayerInfo {
    private int points = 0;
    public int Points {
        get {
            return points;
        }

        set {
            points = value;
        }
    }

    private int balls = 1;
    public int Balls {
        get {
            return balls;
        }

        set {
            balls = value;
        }
    }
}
