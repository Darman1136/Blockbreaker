using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class CGameInfo {
    private int round;
    public int Round {
        set {
            round = value;
        }
        get {
            return round;
        }
    }

    private bool roundOver;
    public bool RoundOver {
        set {
            roundOver = value;
        }
        get {
            return roundOver;
        }
    }

    private bool gameOver;
    public bool GameOver {
        set {
            gameOver = value;
        }
        get {
            return gameOver;
        }
    }

    private bool roundInProgress;
    public bool RoundInProgress {
        get {
            return roundInProgress;
        }
        set {
            roundInProgress = value;
        }
    }

    private bool ballKilledThisRound;
    public bool BallKilledThisRound {
        set {
            ballKilledThisRound = value;
        }
        get {
            return ballKilledThisRound;
        }
    }

    private bool ballKilledByBorderThisRound;
    public bool BallKilledByBorderThisRound {
        set {
            ballKilledByBorderThisRound = value;
        }
        get {
            return ballKilledByBorderThisRound;
        }
    }

    public static float SPAWN_POINT_Y = 0.35f;
    private static float MIN_SPAWN_POINT_X = -2.5f;
    private static float MAX_SPAWN_POINT_X = 2.5f;
    private float spawnPointX = 0f;
    public Vector2 SpawnPoint {
        set {
            if (value.x > MAX_SPAWN_POINT_X) {
                value.x = MAX_SPAWN_POINT_X;
            }
            if (value.x < MIN_SPAWN_POINT_X) {
                value.x = MIN_SPAWN_POINT_X;
            }
            spawnPointX = value.x;
        }
        get {
            return new Vector2(spawnPointX, SPAWN_POINT_Y);
        }
    }

    private int fieldHeight = 7;
    public int FieldHeight {
        set {
            fieldHeight = value;
        }
        get {
            return fieldHeight;
        }
    }

    private int fieldWidth = 7;
    public int FieldWidth {
        set {
            fieldWidth = value;
        }
        get {
            return fieldWidth;
        }
    }

    private int advancedAimLineInRound;
    public int AdvancedAimLineInRound {
        set {
            advancedAimLineInRound = value;
        }
        get {
            return advancedAimLineInRound;
        }
    }

    [NonSerialized]
    private CSpawnableObject[][] field;
    public CSpawnableObject[][] Field {
        set {
            field = value;
        }
        get {
            return field;
        }
    }
}
