using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]

public class CStretchSprite : MonoBehaviour
{

    SpriteRenderer sr;
    EdgeCollider2D edgeCollider;

    void Awake()
    {
        // Get the current sprite with an unscaled size
        sr = GetComponent<SpriteRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        Vector2 p1 = edgeCollider.points[0];
        Vector2 p2 = edgeCollider.points[1];

        Sprite sprite = sr.sprite;
        sprite.bounds.size.Set(10f,1f,0f);
        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);
    }
}
