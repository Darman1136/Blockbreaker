using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKillObjectOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.gameObject.tag;
        if (tag.Equals("PlayerBall"))
        {
            Destroy(coll.gameObject);
        }
    }
}
