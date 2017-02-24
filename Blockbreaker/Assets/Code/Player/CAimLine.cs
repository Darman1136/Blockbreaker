using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CAimLine : MonoBehaviour
{
    private LineRenderer lr;
    private static Vector3 START_POINT = new Vector3(0f, 0.35f, -0.001f);
    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = "AimLine";
        lr = GetComponent<LineRenderer>();
        // lr.numPositions = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 8.3f;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        pos.z = -0.001f;
        Vector3[] positions = new Vector3[] { START_POINT, pos };
        lr.SetPositions(positions);
    }
}
