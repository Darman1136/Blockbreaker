using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CAimLine : MonoBehaviour
{
    private LineRenderer lr;
    /** Only valid points which should be drawn will be stored here. */
    private Vector3[] aimPosition;
    public Vector3[] AimPosition
    {
        get
        {
            return aimPosition;
        }
    }
    /** All values, even if invalid will be stored here. */
    private Vector3[] actualAimPosition;

    private static Vector3 START_POINT = new Vector3(0f, 0.35f, -0.001f);
    private static float MIN_Y_MOUSE_POSITION;
    private static float MAX_Y_MOUSE_POSITION;
    private static float MIN_X_MOUSE_POSITION;
    private static float MAX_X_MOUSE_POSITION;

    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = "AimLine";
        lr = GetComponent<LineRenderer>();

        Transform border = GameObject.Find("TopBorder").GetComponent<Transform>();
        MAX_Y_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).y;
        border = GameObject.Find("BottomBorder").GetComponent<Transform>();
        MIN_Y_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).y;
        border = GameObject.Find("LeftBorder").GetComponent<Transform>();
        MIN_X_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).x;
        border = GameObject.Find("RightBorder").GetComponent<Transform>();
        MAX_X_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).x;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 8.3f;
        Vector3 endPointPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        endPointPosition.z = -0.001f;
        actualAimPosition = new Vector3[] { START_POINT, endPointPosition };
        if (IsValidMousePosition(mousePosition))
        {
            aimPosition = actualAimPosition;
            lr.SetPositions(aimPosition);
        }
    }

    private bool IsValidMousePosition(Vector3 mousePosition)
    {
        Vector3 aimDirection = actualAimPosition[1] - actualAimPosition[0];
        return Vector3.Angle(aimDirection, Vector3.up) < 80f && (mousePosition.y > MIN_Y_MOUSE_POSITION && mousePosition.y < MAX_Y_MOUSE_POSITION && mousePosition.x > MIN_X_MOUSE_POSITION && mousePosition.x < MAX_X_MOUSE_POSITION);
    }
}
