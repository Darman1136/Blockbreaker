using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBall : MonoBehaviour {
    private Rigidbody2D rb;
    private CBallStuckWatchdog watchdog;
    private CDefaultGamemode gamemode;

    private Vector2 initialVelocity;
    public Vector2 InitialVelocity {
        set {
            initialVelocity = value;
        }
    }

    private bool alreadyEnteredBouncePowerUp;
    private bool bounce;
    public bool Bounce {
        set {
            if (value && !alreadyEnteredBouncePowerUp) {
                alreadyEnteredBouncePowerUp = true;
                bounce = true;
            } else if (!value) {
                bounce = false;
            }
        }
        get {
            return bounce;
        }
    }

    private bool duplicate;
    public bool Duplicate {
        set {
            duplicate = value;
        }
        get {
            return duplicate;
        }
    }

    private static float SPEED = 10f;
    private static float MIN_SPEED = 0.3f;
    private static float MIN_SPEED_BEFORE_CONSIDERED_STUCK = 0.35f;
    private static float UNSTUCK_BOOST = 7f;
    private static float[] DIRECTION = { -1, 1 };

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.velocity = initialVelocity;
        }
        watchdog = GameObject.Find("Gamemode").GetComponent<CBallStuckWatchdog>();
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();

        bounce = false;
        alreadyEnteredBouncePowerUp = false;
    }

    void FixedUpdate() {
        rb.velocity = rb.velocity.normalized * SPEED;
        if (rb.velocity.y < MIN_SPEED_BEFORE_CONSIDERED_STUCK) {
            watchdog.AddPossibleStuckBall(this);
        } else {
            watchdog.RemovePossibleStuckBall(this);
        }
        if (rb.velocity.y < MIN_SPEED && rb.velocity.y > -MIN_SPEED) {
            float signXVel = Mathf.Sign(rb.velocity.x);
            float signYVel = Mathf.Sign(rb.velocity.y);
            rb.velocity = new Vector2(signXVel * SPEED - MIN_SPEED, signYVel * MIN_SPEED);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        string tag = coll.gameObject.tag;
        if (tag.Equals("Box")) {
            watchdog.RemovePossibleStuckBall(this);
            coll.gameObject.SendMessage("Hit", 1);
        }
    }

    public Vector2 AngleVelocityByDegree(float degree) {
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = Quaternion.Euler(0, 0, degree) * currentVelocity;
        return rb.velocity;
    }

    public Vector2 AngleVelocityByDegree(float degree, Vector2 velocityToChange) {
        return Quaternion.Euler(0, 0, degree) * velocityToChange;
    }

    public void UnstuckMe() {
        rb.velocity = new Vector2(DIRECTION[UnityEngine.Random.Range(0, 2)] * rb.velocity.x, UNSTUCK_BOOST);
    }

    public void KillBall(bool killedByBorder) {
        gamemode.CheckRoundOver(this, killedByBorder);
        Destroy(this.gameObject);
    }
}
