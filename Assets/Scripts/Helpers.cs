using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private const float GIZMO_DISK_THICKNESS = 0.01f;

    public static void DrawWireDisk(Vector3 position, float radius, Color color)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = color;
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, 1, GIZMO_DISK_THICKNESS));
        Gizmos.DrawWireSphere(Vector3.zero, radius);
        Gizmos.matrix = oldMatrix;
        Gizmos.color = oldColor;
    }

    public static void DrawFilledDisk(Vector3 position, float radius, Color color)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = color;
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, 1, GIZMO_DISK_THICKNESS));
        Gizmos.DrawSphere(Vector3.zero, radius);
        Gizmos.matrix = oldMatrix;
        Gizmos.color = oldColor;
    }

    public static void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1.0f)
    {
        DrawArrow(pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowPosition);
    }

    public static void DrawArrowFromPoints(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1.0f)
    {
        DrawArrowFromPoints(from, to, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowPosition);
    }

    public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1.0f)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);
        DrawArrowEnd(true, pos, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        Gizmos.color = oldColor;
    }

    public static void DrawArrowFromPoints(Vector3 from, Vector3 to, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 1.0f)
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawLine(from, to);
        Vector3 direction = to - from;
        DrawArrowEnd(true, from, direction, color, arrowHeadLength, arrowHeadAngle, arrowPosition);
        Gizmos.color = oldColor;
    }

    private static void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f, float arrowPosition = 0.5f)
    {
        Vector3 right = (Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
        Vector3 left = (Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
        Vector3 up = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;
        Vector3 down = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;

        Vector3 arrowTip = pos + (direction * arrowPosition);

        if (gizmos)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(arrowTip, right);
            Gizmos.DrawRay(arrowTip, left);
            Gizmos.DrawRay(arrowTip, up);
            Gizmos.DrawRay(arrowTip, down);
        }
        else
        {
            Debug.DrawRay(arrowTip, right, color);
            Debug.DrawRay(arrowTip, left, color);
            Debug.DrawRay(arrowTip, up, color);
            Debug.DrawRay(arrowTip, down, color);
        }
    }
}
