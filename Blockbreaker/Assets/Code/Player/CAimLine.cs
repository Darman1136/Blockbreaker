using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CAimLine : MonoBehaviour {
    enum Direction {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    };

    private CDefaultGamemode gamemode;
    private LineRenderer lr;

    /** Only valid points which should be drawn will be stored here. */
    private Vector3[] drawnAimPosition;
    public Vector3[] AimPosition {
        get {
            return drawnAimPosition;
        }
    }
    /** All values, even if invalid will be stored here. */
    private Vector3[] actualAimPosition;

    private static float MIN_Y_MOUSE_POSITION;
    private static float MAX_Y_MOUSE_POSITION;
    private static float MIN_X_MOUSE_POSITION;
    private static float MAX_X_MOUSE_POSITION;

    private static Vector3 RIGHT_BORDER_POSITION = new Vector3(2.6f, 0, 0);
    private static Vector3 LEFT_BORDER_POSITION = new Vector3(-2.6f, 0, 0);
    private static Vector3 TOP_BORDER_POSITION = new Vector3(0, 6.1f, 0);
    private static Vector3 BOTTOM_BORDER_POSITION = new Vector3(0, 0, 0);

    private static float MAX_AIM_ANGLE = 77f;

    void Start() {
        GetComponent<Renderer>().sortingLayerName = "AimLine";
        lr = GetComponent<LineRenderer>();
        Material whiteDiffuseMat = new Material(Shader.Find("Sprites/Default"));
        lr.material = whiteDiffuseMat;
        gamemode = GameObject.Find("Gamemode").GetComponent<CDefaultGamemode>();
        Transform border = GameObject.Find("TopBorder").GetComponent<Transform>();
        MAX_Y_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).y;
        border = GameObject.Find("BottomBorder").GetComponent<Transform>();
        MIN_Y_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).y;
        border = GameObject.Find("LeftBorder").GetComponent<Transform>();
        MIN_X_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).x;
        border = GameObject.Find("RightBorder").GetComponent<Transform>();
        MAX_X_MOUSE_POSITION = Camera.main.WorldToScreenPoint(border.position).x;
    }

    void Update() {
        if (!gamemode.GameInfo.GameOver && !gamemode.GameInfo.RoundInProgress) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 8.3f;
            Vector3 endPointPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            endPointPosition.z = -0.001f;


            Vector3[] lines = FindReflectionLine(endPointPosition);
            int numPoints = 2;
            if (lines != null) {
                actualAimPosition = lines;
                numPoints = 4;
            } else {
                actualAimPosition = new Vector3[] { GetLineStartPoint(), endPointPosition };
            }


            if (IsValidMousePosition(mousePosition)) {
                drawnAimPosition = actualAimPosition;
                lr.SetPositions(drawnAimPosition);
                lr.numPositions = numPoints;
            }
        } else {
            lr.numPositions = 0;
        }
    }

    private void OnDestroy() {
        Destroy(lr.material);
    }

    private Vector3[] FindReflectionLine(Vector3 endPointPosition) {
        Vector3[] calculatedReflectionLine = CalculateRightIntersection(GetLineStartPoint(), endPointPosition - GetLineStartPoint(), endPointPosition, Vector3.up);
        if (calculatedReflectionLine != null) {
            return calculatedReflectionLine;
        }
        calculatedReflectionLine = CalculateLeftIntersection(GetLineStartPoint(), endPointPosition - GetLineStartPoint(), endPointPosition, Vector3.up);
        if (calculatedReflectionLine != null) {
            return calculatedReflectionLine;
        }
        calculatedReflectionLine = CalculateTopIntersection(GetLineStartPoint(), endPointPosition - GetLineStartPoint(), endPointPosition, Vector3.left);
        if (calculatedReflectionLine != null) {
            return calculatedReflectionLine;
        }
        return null;
    }

    private Vector3[] CalculateRightIntersection(Vector3 vec1pos, Vector3 vec1dir, Vector3 vec2pos, Vector3 vec2dir) {
        Vector3 vec3pos = CalculateIntersection(vec1pos, vec1dir, RIGHT_BORDER_POSITION, vec2dir);
        vec3pos.z = -0.001f;

        if (vec3pos.y < 6.1 && vec3pos.y > CGameInfo.SPAWN_POINT_Y) {
            float reflection = Vector3.Angle(vec1dir, Vector3.left) * 2;
            Vector3 vec3dir = Quaternion.Euler(0, 0, reflection) * vec1dir;

            Vector3 vec4pos = CalculateIntersection(vec3pos, vec3dir, LEFT_BORDER_POSITION, vec2dir);
            vec4pos.z = -0.001f;
            vec4pos = ClampLastLinePoint(vec4pos, Direction.LEFT);
            return new Vector3[] { vec1pos, vec2pos, vec3pos, vec4pos };
        }
        return null;
    }

    private Vector3[] CalculateLeftIntersection(Vector3 vec1pos, Vector3 vec1dir, Vector3 vec2pos, Vector3 vec2dir) {
        Vector3 vec3pos = CalculateIntersection(vec1pos, vec1dir, LEFT_BORDER_POSITION, vec2dir);
        vec3pos.z = -0.001f;

        if (vec3pos.y < 6.1 && vec3pos.y > CGameInfo.SPAWN_POINT_Y) {
            float reflection = Vector3.Angle(vec1dir, Vector3.right) * -2;
            Vector3 vec3dir = Quaternion.Euler(0, 0, reflection) * vec1dir;

            Vector3 vec4pos = CalculateIntersection(vec3pos, vec3dir, RIGHT_BORDER_POSITION, vec2dir);
            vec4pos.z = -0.001f;
            vec4pos = ClampLastLinePoint(vec4pos, Direction.RIGHT);
            return new Vector3[] { vec1pos, vec2pos, vec3pos, vec4pos };
        }
        return null;
    }

    private Vector3[] CalculateTopIntersection(Vector3 vec1pos, Vector3 vec1dir, Vector3 vec2pos, Vector3 vec2dir) {
        Vector3 vec3pos = CalculateIntersection(vec1pos, vec1dir, TOP_BORDER_POSITION, vec2dir);
        vec3pos.z = -0.001f;

        if (vec3pos.x < 2.75 && vec3pos.x > -2.75f) {
            float angleMultiply = -1 * Mathf.Sign(vec3pos.x - vec1pos.x);
            float reflection = Vector3.Angle(vec1dir, Vector3.down) * 2 * angleMultiply;
            Vector3 vec3dir = Quaternion.Euler(0, 0, reflection) * vec1dir;

            Vector3 vec4pos = CalculateIntersection(vec3pos, vec3dir, BOTTOM_BORDER_POSITION, vec2dir);
            vec4pos.z = -0.001f;
            vec4pos = ClampLastLinePoint(vec4pos, Direction.BOTTOM);
            return new Vector3[] { vec1pos, vec2pos, vec3pos, vec4pos };
        }
        return null;
    }

    private Vector3 CalculateIntersection(Vector3 vec1pos, Vector3 vec1dir, Vector3 vec2pos, Vector3 vec2dir) {
        Vector3 vec3dir = vec2pos - vec1pos;
        Vector3 crossVec1and2 = Vector3.Cross(vec1dir, vec2dir);
        Vector3 crossVec3and2 = Vector3.Cross(vec3dir, vec2dir);

        float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;

        return vec1pos + (vec1dir * s);
    }

    private Vector3 ClampLastLinePoint(Vector3 vec4pos, Direction lastPointSide) {
        switch (lastPointSide) {
            case Direction.LEFT:
                if (vec4pos.x < -2.75f) {
                    vec4pos.x = -2.75f;
                }
                break;
            case Direction.RIGHT:
                if (vec4pos.x > 2.75f) {
                    vec4pos.x = 2.75f;
                }
                break;
            case Direction.BOTTOM:
                if (vec4pos.y < 0f) {
                    vec4pos.y = 0f;
                }
                break;
        }
        return vec4pos;
    }

    private bool IsValidMousePosition(Vector3 mousePosition) {
        Vector3 aimDirection = actualAimPosition[1] - actualAimPosition[0];
        return Vector3.Angle(aimDirection, Vector3.up) < MAX_AIM_ANGLE && (mousePosition.y > MIN_Y_MOUSE_POSITION && mousePosition.y < MAX_Y_MOUSE_POSITION && mousePosition.x > MIN_X_MOUSE_POSITION && mousePosition.x < MAX_X_MOUSE_POSITION);
    }

    private Vector3 GetLineStartPoint() {
        Vector2 spawnPoint = gamemode.GameInfo.SpawnPoint;
        return new Vector3(spawnPoint.x, spawnPoint.y, -0.001f);
    }
}
