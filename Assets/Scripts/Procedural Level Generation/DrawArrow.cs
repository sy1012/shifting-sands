using UnityEngine;
using System.Collections;

public static class DrawArrow
{
    public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }

    public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
        Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
        Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
    }

    public static void ForDebug(Vector3 pos, Vector3 direction, Color color)
    {
        float arrowHeadLength = 0.2f;float arrowHeadAngle = 25.0f;
        Debug.DrawRay(pos, direction,color);
        Quaternion arrowRot = Quaternion.FromToRotation(Vector3.up, direction);
        Vector3 right = arrowRot * Quaternion.Euler(0, 0, 180+arrowHeadAngle) * new Vector3(0, 1, 0);
        Vector3 left = arrowRot * Quaternion.Euler(0, 0, 180 - arrowHeadAngle) * new Vector3(0, 1, 0);
        Debug.DrawRay(pos + direction, right * arrowHeadLength,color);
        Debug.DrawRay(pos + direction, left * arrowHeadLength,color);
    }
}
