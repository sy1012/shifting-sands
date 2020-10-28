using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oasis : MonoBehaviour
{

    private float radius = 4f;
    private int maxPyramids = 5;
    private int minPyramids = 3;
    public bool generated = false;

    public float getRadius()
    {
        return radius;
    }

    public void setRadius(float rad)
    {
        radius = rad;
    }

    public int getMaxPyramids()
    {
        return maxPyramids;
    }
    public int getMinPyramids()
    {
        return minPyramids;
    }
    public void setMaxPyramids(int max)
    {
        maxPyramids = max;
    }
    public void setMinPyramids(int min)
    {
        minPyramids = min;
    }

}
