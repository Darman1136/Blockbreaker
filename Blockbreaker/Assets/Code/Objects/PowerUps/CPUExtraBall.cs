using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUExtraBall : CPowerUp {
    public GameObject objectToSpawn;
    private float[] direction = { -1f, 1f };
    public override void Start() {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag.Equals("PlayerBall")) {
            CBall ball = collision.gameObject.GetComponent<CBall>();
            if (!ball.Duplicate) {
                float angle = direction[UnityEngine.Random.Range(0, 2)] * 45;
                Vector2 newVelocity = ball.AngleVelocityByDegree(angle);
                SpawnDuplicate(ball.transform.position, newVelocity, angle);
            }
            DestoryAtEndOfRound = true;
        }
    }

    private void SpawnDuplicate(Vector2 spawnPosition, Vector2 newVelocity, float angle) {
        if (objectToSpawn != null) {
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            CBall duplicate = spawnedObject.GetComponent<CBall>();
            duplicate.Duplicate = true;
            duplicate.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0f, 0.8f, 1f); ;
            duplicate.InitialVelocity = duplicate.AngleVelocityByDegree(-angle * 2, newVelocity);
        }
    }
}
