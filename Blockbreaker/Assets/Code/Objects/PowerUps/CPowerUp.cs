using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]

abstract public class CPowerUp : CSpawnableObject {
    private bool destroyAtEndOfRound;
    public bool DestoryAtEndOfRound {
        set {
            destroyAtEndOfRound = value;
        }
        get {
            return destroyAtEndOfRound;
        }
    }

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public virtual void Start() {
        destroyAtEndOfRound = false;
    }

    public void KillPowerUp() {
        Destroy(this.gameObject);
    }

    public override CSerializableSpawnableObject GetSerializableObject() {
        CSerializableSpawnableObject sso = new CSerializableSpawnableObject();
        sso.data.Add("destroyAtEndOfRound", destroyAtEndOfRound);
        return sso;
    }
}
