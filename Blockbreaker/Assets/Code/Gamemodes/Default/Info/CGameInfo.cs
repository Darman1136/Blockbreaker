using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameInfo : MonoBehaviour {
    private int round;
    public int Round
    {
        set
        {
            round = value;
        }
        get
        {
            return round;
        }
    }
}
