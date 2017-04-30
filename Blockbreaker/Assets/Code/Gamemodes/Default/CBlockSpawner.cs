using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CBlockSpawner : MonoBehaviour {
    private CDefaultGamemode gamemode;

    private static float FIRST_SPAWNPOINT_Y = 6.08f;
    private static float FIRST_SPAWNPOINT_X = -2.8f;
    private static float SPACING_TO_NEXT_SPAWNPOINT = .93f;

    public CBlock BlockPrefab;
    public CPowerUp PUBouncePrefab;
    public CPowerUp PUExtraBallPrefab;
    public CPowerUp PUKillBallPrefab;
    public CPowerUp PUNewBallPrefab;
    public CPowerUp PUAdvancedAimLine;

    private CSpawnableObject[] powerUps;

    void Start() {
        powerUps = new CSpawnableObject[] { PUBouncePrefab, PUExtraBallPrefab, PUKillBallPrefab, PUAdvancedAimLine };
        gamemode = CDefaultGamemode.GAMEMODE;
    }

    public CSpawnableObject[] SpawnNextRound(int round) {
        CSpawnableObject[] newObjects = new CSpawnableObject[gamemode.GameInfo.FieldHeight];

        SpawnNewBallPU(newObjects);

        SpawnBlocks(newObjects, round);

        SpawnPowerUps(newObjects);

        CheckIfObjectsSpawned(newObjects, round);
        return newObjects;
    }

    private void SpawnPowerUps(CSpawnableObject[] newObjects) {
        int position = UnityEngine.Random.Range(0, gamemode.GameInfo.FieldWidth);
        if (CanSpawnAtPosition(newObjects, position)) {
            CSpawnableObject powerUpToSpawn = powerUps[UnityEngine.Random.Range(0, powerUps.Length)];
            InstantiateObject(newObjects, position, powerUpToSpawn);
        }
    }

    private bool CanSpawnAtPosition(CSpawnableObject[] newObjects, int arrayPosition) {
        if (newObjects[arrayPosition] == null) {
            return true;
        }
        return false;
    }

    private void SpawnBlocks(CSpawnableObject[] newObjects, int health) {
        for (int i = 0; i < gamemode.GameInfo.FieldWidth; i++) {
            if (CanSpawnAtPosition(newObjects, i) && UnityEngine.Random.Range(0, 2) == 1) {
                SpawnBlock(newObjects, i, health);
            }
        }
    }

    /**
     * There may not be spawned another object at the returned position.
     */
    private void SpawnNewBallPU(CSpawnableObject[] newObjects) {
        int position = UnityEngine.Random.Range(0, gamemode.GameInfo.FieldWidth);
        InstantiateObject(newObjects, position, PUNewBallPrefab);
    }

    public void MoveGameObjects(CSpawnableObject[] array) {
        if (array != null) {
            foreach (CSpawnableObject go in array) {
                if (go != null) {
                    go.transform.Translate(new Vector2(0f, -SPACING_TO_NEXT_SPAWNPOINT));
                }
            }
        }
    }

    private void CheckIfObjectsSpawned(CSpawnableObject[] newObjects, int health) {
        foreach (CSpawnableObject go in newObjects) {
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
        SpawnBlock(newObjects, position, health * 2);
    }

    private void SpawnBlock(CSpawnableObject[] newObjects, int arrayPosition, int health) {
        InstantiateObject(newObjects, arrayPosition, BlockPrefab);
        newObjects[arrayPosition].GetComponent<CBlock>().Health = health;
    }

    public CBlock SpawnBlockOnLoad(int arrayPosition) {
        return (CBlock)InstantiateObjectOnLoad(arrayPosition, BlockPrefab);
    }

    private void InstantiateObject(CSpawnableObject[] newObjects, int arrayPosition, CSpawnableObject objectToSpawn) {
        newObjects[arrayPosition] = Instantiate(objectToSpawn, new Vector2(FIRST_SPAWNPOINT_X + SPACING_TO_NEXT_SPAWNPOINT * arrayPosition, FIRST_SPAWNPOINT_Y), Quaternion.identity);

    }

    private CSpawnableObject InstantiateObjectOnLoad(int arrayPosition, CSpawnableObject objectToSpawn) {
        return Instantiate(objectToSpawn, new Vector2(FIRST_SPAWNPOINT_X + SPACING_TO_NEXT_SPAWNPOINT * arrayPosition, FIRST_SPAWNPOINT_Y), Quaternion.identity);
    }

    public CPUNewBall SpawnCPUNewBallOnLoad(int arrayPosition) {
        return (CPUNewBall)InstantiateObjectOnLoad(arrayPosition, PUNewBallPrefab);
    }

    public CPUKillBall SpawnCPUKillBallOnLoad(int arrayPosition) {
        return (CPUKillBall)InstantiateObjectOnLoad(arrayPosition, PUKillBallPrefab);
    }

    public CPUExtraBall SpawnCPUExtraBallnOnLoad(int arrayPosition) {
        return (CPUExtraBall)InstantiateObjectOnLoad(arrayPosition, PUExtraBallPrefab);
    }

    public CPUBounce SpawnCPUBounceOnLoad(int arrayPosition) {
        return (CPUBounce)InstantiateObjectOnLoad(arrayPosition, PUBouncePrefab);
    }

    public CPUAdvancedAimLine SpawnCPUAdvancedAimLineOnLoad(int arrayPosition) {
        return (CPUAdvancedAimLine)InstantiateObjectOnLoad(arrayPosition, PUAdvancedAimLine);
    }
}
