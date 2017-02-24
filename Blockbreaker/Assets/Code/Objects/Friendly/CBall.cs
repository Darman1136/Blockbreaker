using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 initialVelocity;

    private float speed = 10;

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
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }

    public void SetInitialVelocity(Vector2 vec)
    {
        initialVelocity = vec;
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
