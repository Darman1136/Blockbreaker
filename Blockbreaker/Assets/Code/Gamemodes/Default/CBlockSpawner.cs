using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlockSpawner : MonoBehaviour
{
    private static float FIRST_SPAWNPOINT_Y = 5.75f;
    private static float FIRST_SPAWNPOINT_X = -2.25f;
    private static float SPACING_TO_NEXT_SPAWNPOINT = .75f;

    public GameObject objectToSpawn;

    public GameObject[] SpawnNextRound(int round)
    {
        GameObject[] newObjects = new GameObject[7];
        for (int i = 0; i < 7; i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                InstantiateObject(newObjects, i, round);
            }
        }
        CheckIfObjectsSpawned(newObjects, round);
        return newObjects;
    }

    public void MoveGameObjects(GameObject[] array)
    {
        if (array != null)
        {
            foreach (GameObject go in array)
            {
                if (go != null)
                {
                    go.transform.Translate(new Vector2(0f, -SPACING_TO_NEXT_SPAWNPOINT));
                }
            }
        }
    }

    private void CheckIfObjectsSpawned(GameObject[] newObjects, int round)
    {
        foreach (GameObject go in newObjects)
        {
            if (go != null)
            {
                return;
            }
        }

        /* Spawn at least one block at a random position with double the health. */
        int position = UnityEngine.Random.Range(0, 7);
        InstantiateObject(newObjects, position, round * 2);
    }

    private void InstantiateObject(GameObject[] newObjects, int arrayPosition, int round)
    {
        newObjects[arrayPosition] = Instantiate(objectToSpawn, new Vector2(FIRST_SPAWNPOINT_X + SPACING_TO_NEXT_SPAWNPOINT * arrayPosition, FIRST_SPAWNPOINT_Y), Quaternion.identity);
        newObjects[arrayPosition].GetComponent<CBlock>().Health = round;
    }
}
