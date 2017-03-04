using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKillObjectOnCollision : MonoBehaviour
{
    private CDefaultGamemode gamemode;

    void Start()
    {
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.gameObject.tag;
        if (tag.Equals("PlayerBall"))
        {
            CBall ball = coll.gameObject.GetComponent<CBall>();
            if(ball.Bounce)
            {
                ball.Bounce = false;
            }
            else
            {
                Destroy(coll.gameObject);
                gamemode.CheckRoundOver();
            }
        }
    }
}
