using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlockSpawner : MonoBehaviour {
    private CDefaultGamemode gamemode;

    private static float FIRST_SPAWNPOINT_Y = 5.15f;
    private static float FIRST_SPAWNPOINT_X = -2.25f;
    private static float SPACING_TO_NEXT_SPAWNPOINT = .75f;

    public GameObject BlockPrefab;
    public GameObject PUBouncePrefab;
    public GameObject PUExtraBallPrefab;
    public GameObject PUKillBallPrefab;
    public GameObject PUNewBallPrefab;

    public GameObject[] powerUps;

    void Start() {
        powerUps = new GameObject[] { PUBouncePrefab, PUExtraBallPrefab, PUKillBallPrefab };
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
    }

    public GameObject[] SpawnNextRound(int round) {
        GameObject[] newObjects = new GameObject[gamemode.GameInfo.FieldHeight];

        SpawnNewBallPU(newObjects);

        SpawnBlocks(newObjects, round);

        SpawnPowerUps(newObjects);

        CheckIfObjectsSpawned(newObjects, round);
        return newObjects;
    }

    private void SpawnPowerUps(GameObject[] newObjects) {
        int position = UnityEngine.Random.Range(0, gamemode.GameInfo.FieldWidth);
        if (CanSpawnAtPosition(newObjects, position)) {
            GameObject powerUpToSpawn = powerUps[UnityEngine.Random.Range(0, powerUps.Length)];
            InstantiateObject(newObjects, position, powerUpToSpawn);
        }
    }

    private bool CanSpawnAtPosition(GameObject[] newObjects, int arrayPosition) {
        if (newObjects[arrayPosition] == null) {
            return true;
        }
        return false;
    }

    private void SpawnBlocks(GameObject[] newObjects, int health) {
        for (int i = 0; i < gamemode.GameInfo.FieldWidth; i++) {
            if (CanSpawnAtPosition(newObjects, i) && UnityEngine.Random.Range(0, 2) == 1) {
                SpawnBlock(newObjects, i, BlockPrefab, health);
            }
        }
    }

    /**
     * There may not be spawned another object at the returned position.
     */
    private void SpawnNewBallPU(GameObject[] newObjects) {
        int position = UnityEngine.Random.Range(0, gamemode.GameInfo.FieldWidth);
        InstantiateObject(newObjects, position, PUNewBallPrefab);
    }

    public void MoveGameObjects(GameObject[] array) {
        if (array != null) {
            foreach (GameObject go in array) {
                if (go != null) {
                    go.transform.Translate(new Vector2(0f, -SPACING_TO_NEXT_SPAWNPOINT));
                }
            }
        }
    }

    private void CheckIfObjectsSpawned(GameObject[] newObjects, int health) {
        foreach (GameObject go in newObjects) {
            if (go != null && !go.tag.Equals("PowerUp")) {
                return;
            }
        }

        /* Spawn at least one block at a random position with double the health. */
        int tries = 100;
        int position;
        do {
            position = UnityEngine.Random.Range(0, gamemode.GameInfo.FieldWidth);
        } while (!CanSpawnAtPosition(newObjects, position) && --tries > 0);
        if (tries <= 0) {
            Debug.LogError("Error when spawning at least one block. Didn't find a valid position.");
        }
        SpawnBlock(newObjects, position, BlockPrefab, health * 2);
    }

    private void SpawnBlock(GameObject[] newObjects, int arrayPosition, GameObject objectToSpawn, int health) {
        InstantiateObject(newObjects, arrayPosition, BlockPrefab);
        newObjects[arrayPosition].GetComponent<CBlock>().Health = health;
    }

    private void InstantiateObject(GameObject[] newObjects, int arrayPosition, GameObject objectToSpawn) {
        newObjects[arrayPosition] = Instantiate(objectToSpawn, new Vector2(FIRST_SPAWNPOINT_X + SPACING_TO_NEXT_SPAWNPOINT * arrayPosition, FIRST_SPAWNPOINT_Y), Quaternion.identity);

    }
}
