using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerInfo : MonoBehaviour
{
    private int points;
    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }

    private int balls;
    public int Balls
    {
        get
        {
            return balls;
        }

        set
        {
            balls = value;
        }
    }
}
