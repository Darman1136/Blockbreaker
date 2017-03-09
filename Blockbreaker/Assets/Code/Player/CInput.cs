﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CInput : MonoBehaviour {
    private CDefaultGamemode gamemode;
    private CAimLine aimLine;
    public GameObject Spawner;

    void Start() {
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
        aimLine = GameObject.Find("AimLine").GetComponent<CAimLine>();
    }

    void Update() {
        if (!gamemode.GameInfo.GameOver && Input.GetMouseButtonUp(0) && !gamemode.GameInfo.RoundInProgress) {
            gamemode.GameInfo.RoundInProgress = true;
            CreateSpawner();
        }

        if(Input.GetAxisRaw("Cancel") == 1) {
            SceneManager.LoadScene(0);
        }
    }

    private void CreateSpawner() {
        if (aimLine.AimPosition.Length >= 2) {
            Vector2 spawnVelocity = aimLine.AimPosition[1] - aimLine.AimPosition[0];

            CTimedSpawner spawner = Instantiate(Spawner, new Vector2(9999f, 9999f), Quaternion.identity).GetComponent<CTimedSpawner>();
            spawner.InitialVelocity = spawnVelocity;
        }
    }
}
