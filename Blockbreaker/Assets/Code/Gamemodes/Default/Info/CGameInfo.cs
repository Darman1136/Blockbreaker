﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameInfo : MonoBehaviour
{
    private int round;
    public int Round
    {
        set
        {
            round = value;
        }
        get
        {
            return round;
        }
    }

    private bool ballKilledByBorderThisRound;
    public bool BallKilledByBorderThisRound
    {
        set
        {
            ballKilledByBorderThisRound = value;
        }
        get
        {
            return ballKilledByBorderThisRound;
        }
    }

    public static float SPAWN_POINT_Y = 0.35f;
    private Vector2 spawnPoint = new Vector2(0f, SPAWN_POINT_Y);
    public Vector2 SpawnPoint
    {
        set
        {
            spawnPoint = value;
        }
        get
        {
            return spawnPoint;
        }
    }

    private int fieldHeight = 7;
    public int FieldHeight
    {
        set
        {
            fieldHeight = value;
        }
        get
        {
            return fieldHeight;
        }
    }

    private int fieldWidth = 7;
    public int FieldWidth
    {
        set
        {
            fieldWidth = value;
        }
        get
        {
            return fieldWidth;
        }
    }
}
