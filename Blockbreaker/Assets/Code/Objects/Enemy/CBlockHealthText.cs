using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]

public class CBlockHealthText : MonoBehaviour {
    private string initialText;
    private TextMesh tm;

	// Use this for initialization
	void Awake () {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "BlockText";
        tm = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInitialText(string initialText)
    {
        tm.text = initialText;
    }

    public void UpdateText(string text)
    {
        tm.text = text;
    } 
}

