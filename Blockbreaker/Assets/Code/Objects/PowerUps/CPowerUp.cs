using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]

abstract public class CPowerUp : MonoBehaviour {

    private bool destroyAtEndOfRound;
    public bool DestoryAtEndOfRound {
        set {
            destroyAtEndOfRound = value;
        }
        get {
            return destroyAtEndOfRound;
        }
    }
    // Use this for initialization
    public virtual void Start() {
        destroyAtEndOfRound = false;
    }

    public void KillPowerUp() {
        Destroy(this.gameObject);
    }
}
