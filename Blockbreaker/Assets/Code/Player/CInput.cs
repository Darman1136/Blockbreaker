using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInput : MonoBehaviour
{
    private CDefaultGamemode gamemode;
    private CAimLine aimLine;
    public GameObject Spawner;

    // Use this for initialization
    void Start()
    {
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
        aimLine = GameObject.Find("AimLine").GetComponent<CAimLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gamemode.RoundInProgress)
        {
            gamemode.RoundInProgress = true;
            CreateSpawner();
        }
    }

    private void CreateSpawner()
    {
        if (aimLine.AimPosition.Length == 2)
        {
            Vector2 spawnVelocity = aimLine.AimPosition[1] - aimLine.AimPosition[0];

            CTimedSpawner spawner = Instantiate(Spawner, new Vector2(9999f, 9999f), Quaternion.identity).GetComponent<CTimedSpawner>();
            spawner.InitialVelocity = spawnVelocity;
        }
    }
}
