using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDefaultGamemode : MonoBehaviour
{
    private int round = 0;
    public int Round
    {
        get
        {
            return this.round;
        }
    }

    private static int FIELD_SIZE = 8;
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

    private Canvas canvasGameOver;
    private CBlockSpawner bs;

    private GameObject[][] field;

    void Start()
    {
        roundOver = true;
        roundInProgress = true;
        gameOver = false;
        field = new GameObject[FIELD_SIZE][];

        canvasGameOver = GameObject.Find("CanvasGameOver").GetComponent<Canvas>();
        bs = GetComponent<CBlockSpawner>();
    }

    void Update()
    {
        if (IsRoundOver())
        {
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

        round++;
        GameObject[] newObjects = bs.SpawnNextRound(round);
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
        if (field[FIELD_SIZE - 1] != null)
        {
            gameOver = true;
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

    public void SetRoundIsOver()
    {
        roundOver = true;
    }
}
