using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    /// <summary>
    /// Always returns an angle between 0 and 360
    /// </summary>
    /// <param name="A"></param> Vector 3 from
    /// <param name="B"></param> Vector 3 to
    /// <returns></returns> CW angle between 0 and 360
    public static float PositiveCWAngleBetween(Vector3 A, Vector3 B)
    {
        float angle = Vector3.SignedAngle(A, B,-Vector3.forward);
        if (angle <0)
        {
            angle += 360;
        }
        return angle;
    }
    public static Vector3 AverageVector3(List<Vector3> vector3s)
    {
        if (vector3s.Count == 0)
        {
            return Vector3.zero;
        }
        float xsum = 0;
        float ysum = 0;
        float zsum = 0;
        foreach (var Vec in vector3s)
        {
            xsum += Vec.x;
            ysum += Vec.y;
            zsum += Vec.z;
        }
        return new Vector3(xsum, ysum, zsum) / vector3s.Count;
    }
}
