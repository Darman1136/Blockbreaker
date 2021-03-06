﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimedSpawner : MonoBehaviour {
    private CDefaultGamemode gamemode;

    private static float MAX_DELAY = 0.07f;

    private Vector2 spawnPoint;
    private float currentDelay = MAX_DELAY;
    private int spawnedObjectsCount = 0;
    private Vector2 initialVelocity;
    public Vector2 InitialVelocity {
        set {
            this.initialVelocity = value;
        }
    }

    public GameObject objectToSpawn;

    void Start() {
        gamemode = CDefaultGamemode.GAMEMODE;
        spawnPoint = gamemode.GameInfo.SpawnPoint;
        SpawnBall();
    }

    void Update() {
        if (spawnedObjectsCount < gamemode.PlayerInfo.Balls) {
            if (timerTick(Time.deltaTime)) {
                SpawnBall();
            }
        } else {
            Destroy(this.gameObject);
        }
    }


    private void SpawnBall() {
        if (objectToSpawn != null) {
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
            spawnedObject.GetComponent<CBall>().InitialVelocity = initialVelocity;
            spawnedObjectsCount++;
        }
    }

    private bool timerTick(float passedMs) {
        currentDelay -= passedMs;
        if (currentDelay <= 0) {
            resetTimer();
            return true;
        }
        return false;
    }

    private void resetTimer() {
        currentDelay = MAX_DELAY + currentDelay;
    }
}
