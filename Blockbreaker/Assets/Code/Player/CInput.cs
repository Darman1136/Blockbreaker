using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInput : MonoBehaviour
{
    private static Vector2 START_POINT = new Vector2(0f, 0.35f);
    public GameObject objectToSpawn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 8.3f;
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePosition);
            SpawnBall(spawnPos);
        }
    }

    private void SpawnBall(Vector3 vec)
    {
        SpawnBall(new Vector2(vec.x, vec.y));
    }

    private void SpawnBall(Vector2 vec)
    {
        Vector2 spawnVelocity = vec - START_POINT;
        GameObject spawnedObject = Instantiate(objectToSpawn, new Vector2(0f, 0.35f), Quaternion.identity);
        spawnedObject.GetComponent<CBall>().SetInitialVelocity(spawnVelocity);
    }
}
