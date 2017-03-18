using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSpawnableObject : MonoBehaviour {
    public enum Type {
        CBlock,
        CPUAdvancedAimLine,
        CPUBounce,
        CPUExtraBall,
        CPUKillBall,
        CPUNewBall
    }

    public abstract CSerializableSpawnableObject GetSerializableObject();

}
