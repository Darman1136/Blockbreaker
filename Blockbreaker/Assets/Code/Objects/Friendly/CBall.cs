using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBall : MonoBehaviour
{
    private Rigidbody2D rb;
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

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = initialVelocity;
        }
        else
        {
            Debug.LogError("No Rigidbody2D found.");
        }
        bounce = false;
        alreadyEnteredBouncePowerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.gameObject.tag;
        if (tag.Equals("Box"))
        {
            coll.gameObject.SendMessage("Hit", 1);
        }
    }
}
