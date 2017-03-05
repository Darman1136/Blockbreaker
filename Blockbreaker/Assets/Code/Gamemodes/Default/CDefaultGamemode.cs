﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDefaultGamemode : MonoBehaviour
{
    private bool roundOver, gameOver, roundInProgress;
    public bool RoundInProgress
    {
        get
        {
            return this.roundInProgress;
        }
        set
        {
            this.roundInProgress = value;
        }
    }

    private GameObject[][] field;
    private Canvas canvasGameOver;
    private CBlockSpawner bs;
    private CGameInfo gi;
    public CGameInfo GameInfo
    {
        get
        {
            return gi;
        }
    }
    private CPlayerInfo pi;
    public CPlayerInfo PlayerInfo
    {
        get
        {
            return pi;
        }
    }

    void Start()
    {
        roundOver = true;
        roundInProgress = true;
        gameOver = false;

        canvasGameOver = GameObject.Find("CanvasGameOver").GetComponent<Canvas>();
        bs = GetComponent<CBlockSpawner>();
        GameObject goInformation = GameObject.Find("Information");
        gi = goInformation.GetComponent<CGameInfo>();
        pi = goInformation.GetComponent<CPlayerInfo>();

        field = new GameObject[gi.FieldHeight][];
    }

    void Update()
    {
        if (IsRoundOver())
        {
            RemoveAllUsedPowerUps();
            UpdateForNextRound();
            if (IsGameOver())
            {
                GameOver();
                return;
            }

        }
    }

    private void UpdateForNextRound()
    {
        MoveArraysInArray();
        MoveObjectsOnScreen();

        gi.Round = ++gi.Round;
        GameObject[] newObjects = bs.SpawnNextRound(gi.Round);
        field[0] = newObjects;

        roundOver = false;
        roundInProgress = false;
    }

    private void MoveObjectsOnScreen()
    {
        foreach (GameObject[] array in field)
        {
            bs.MoveGameObjects(array);
        }
    }

    private void MoveArraysInArray()
    {
        for (int index = FIELD_SIZE - 2; index >= 0; index--)
        {
            if (!IsArrayEmpty(field[index]))
            {
                field[index + 1] = field[index];
            }
            else
            {
                field[index + 1] = null;
            }
            field[index] = null;
        }
    }

    private bool IsArrayEmpty(GameObject[] array)
    {
        if (array != null)
        {
            foreach (GameObject go in array)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsGameOver()
    {
        if (field[field.Length - 1] != null)
        {
            foreach (GameObject go in field[field.Length - 1])
            {
                if (go != null)
                {
                    if (go.tag.Equals("Box"))
                    {
                        gameOver = true;
                    }
                    else if (go.tag.Equals("PowerUp"))
                    {
                        go.GetComponent<CPowerUp>().DestoryAtEndOfRound = true;
                    }
                }
            }
        }
        return gameOver;
    }

    private void GameOver()
    {
        canvasGameOver.enabled = true;
    }

    private bool IsRoundOver()
    {
        return roundOver;
    }

    public void CheckRoundOver(CBall ball, bool killedByBorder)
    {
        if (killedByBorder && IsFirstBallToBeKilledByBorder())
        {
            GameInfo.SpawnPoint = new Vector2(ball.transform.position.x, CGameInfo.SPAWN_POINT_Y);
        }
        if (GameObject.FindGameObjectsWithTag("PlayerBall").Length - 1 == 0)
        {
            roundOver = true;
            ResetFirstBallToBeKilledByBorder();
        }
    }

    private void ResetFirstBallToBeKilledByBorder()
    {
        GameInfo.BallKilledByBorderThisRound = false;
    }

    private bool IsFirstBallToBeKilledByBorder()
    {
        if(GameInfo.BallKilledByBorderThisRound)
        {
            return false;
        }
        GameInfo.BallKilledByBorderThisRound = true;
        return true;
    }

    private void RemoveAllUsedPowerUps()
    {
        foreach (GameObject[] gos in field)
        {
            if (gos != null)
            {
                for (int index = 0; index < gos.Length; index++)
                {
                    GameObject go = gos[index];
                    if (go != null)
                    {
                        if (go.tag.Equals("PowerUp"))
                        {
                            CPowerUp up=  go.GetComponent<CPowerUp>();
                            if (up.DestoryAtEndOfRound)
                            {
                                gos[index] = null;
                                up.KillPowerUp();
                            }
                        }
                    }
                }
            }
        }
    }
}
