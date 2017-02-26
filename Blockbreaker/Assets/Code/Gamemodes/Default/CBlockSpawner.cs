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

    void Start()
    {

    }

    void Update()
    {

    }

    public GameObject[] SpawnNextRound()
    {
        GameObject[] newObjects = new GameObject[7];
        for (int i = 0; i < 7; i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                newObjects[i] = Instantiate(objectToSpawn, new Vector2(FIRST_SPAWNPOINT_X + SPACING_TO_NEXT_SPAWNPOINT * i, FIRST_SPAWNPOINT_Y), Quaternion.identity);
            }
        }

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
}
