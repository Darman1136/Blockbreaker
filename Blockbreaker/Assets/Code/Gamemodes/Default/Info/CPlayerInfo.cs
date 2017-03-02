using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerInfo : MonoBehaviour
{
    private int points = 0;
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

    private int balls = 1;
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
