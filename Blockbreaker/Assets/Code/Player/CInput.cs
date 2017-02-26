using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInput : MonoBehaviour
{
    private static Vector2 START_POINT = new Vector2(0f, 0.35f);

    private CDefaultGamemode gamemode;
    public GameObject Spawner;

    // Use this for initialization
    void Start()
    {
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
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
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 8.3f;
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 spawnPos2 = new Vector2(spawnPos.x, spawnPos.y);
        Vector2 spawnVelocity = spawnPos2 - START_POINT;

        CTimedSpawner spawner = Instantiate(Spawner, new Vector2(9999f, 9999f), Quaternion.identity).GetComponent<CTimedSpawner>();
        spawner.InitialVelocity = spawnVelocity;
    }
}
