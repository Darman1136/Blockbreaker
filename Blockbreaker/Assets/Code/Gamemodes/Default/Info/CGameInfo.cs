using System.Collections;
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

    private bool roundOver;
    public bool RoundOver
    {
        set
        {
            roundOver = value;
        }
        get
        {
            return roundOver;
        }
    }

    private bool gameOver;
    public bool GameOver
    {
        set
        {
            gameOver = value;
        }
        get
        {
            return gameOver;
        }
    }

    private bool roundInProgress;
    public bool RoundInProgress
    {
        get
        {
            return roundInProgress;
        }
        set
        {
            roundInProgress = value;
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
    private static float MIN_SPAWN_POINT_X = -2.5f;
    private static float MAX_SPAWN_POINT_X = 2.5f;
    private Vector2 spawnPoint = new Vector2(0f, SPAWN_POINT_Y);
    public Vector2 SpawnPoint
    {
        set
        {
            if(value.x > MAX_SPAWN_POINT_X)
            {
                value.x = MAX_SPAWN_POINT_X;
            }
            if (value.x < MIN_SPAWN_POINT_X)
            {
                value.x = MIN_SPAWN_POINT_X;
            }
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
