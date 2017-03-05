using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private CBallStuckWatchdog watchdog;

    private Vector2 initialVelocity;
    public Vector2 InitialVelocity
    {
        set
        {
            initialVelocity = value;
        }
    }
    private float speed = 10;

    private bool alreadyEnteredBouncePowerUp;
    private bool bounce;
    public bool Bounce
    {
        set
        {
            if (value && !alreadyEnteredBouncePowerUp)
            {
                alreadyEnteredBouncePowerUp = true;
                bounce = true;
            }
            else if (!value)
            {
                bounce = false;
            }
        }
        get
        {
            return bounce;
        }
    }

    private bool duplicate;
    public bool Duplicate
    {
        set
        {
            duplicate = value;
        }
        get
        {
            return duplicate;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = initialVelocity;
        }
        watchdog = GameObject.Find("Gamemode").GetComponent<CBallStuckWatchdog>();

        bounce = false;
        alreadyEnteredBouncePowerUp = false;
    }

    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
        if (rb.velocity.y < 0.4f)
        {
            watchdog.AddPossibleStuckBall(this);
        }
        else
        {
            watchdog.RemovePossibleStuckBall(this);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.gameObject.tag;
        if (tag.Equals("Box"))
        {
            watchdog.RemovePossibleStuckBall(this);
            coll.gameObject.SendMessage("Hit", 1);
        }
    }

    public Vector2 AngleVelocityByDegree(float degree)
    {
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = Quaternion.Euler(0, 0, degree) * currentVelocity;
        return rb.velocity;
    }

    public Vector2 AngleVelocityByDegree(float degree, Vector2 velocityToChange)
    {
        return Quaternion.Euler(0, 0, degree) * velocityToChange;
    }

    public void UnstuckMe()
    {
        rb.velocity = new Vector2(rb.velocity.x, 7f);
    }
}
