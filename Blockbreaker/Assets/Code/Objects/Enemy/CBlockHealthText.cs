using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]

public class CBlockHealthText : MonoBehaviour {
    private TextMesh tm;

	void Awake () {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "BlockText";
        tm = GetComponent<TextMesh>();
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

