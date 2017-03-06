using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBallStuckWatchdog : MonoBehaviour {
    private Dictionary<CBall, float> stuckBalls = new Dictionary<CBall, float>();
    private static float TIME_TILL_ASSUMED_STUCK = 7f;

    void Update() {
        List<CBall> toBeRemoved = new List<CBall>();
        foreach (KeyValuePair<CBall, float> entry in stuckBalls) {
            if (Time.realtimeSinceStartup - entry.Value > TIME_TILL_ASSUMED_STUCK) {
                if (entry.Key != null) {
                    entry.Key.UnstuckMe();
                }
                toBeRemoved.Add(entry.Key);
            }
        }

        foreach (CBall ball in toBeRemoved) {
            RemovePossibleStuckBall(ball);
        }

    }

    public void AddPossibleStuckBall(CBall ball) {
        if (!stuckBalls.ContainsKey(ball)) {
            stuckBalls[ball] = Time.realtimeSinceStartup;
        }
    }

    public void RemovePossibleStuckBall(CBall ball) {
        if (stuckBalls.ContainsKey(ball)) {
            stuckBalls.Remove(ball);
        }
    }
}
