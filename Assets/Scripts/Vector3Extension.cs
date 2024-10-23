using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static float CompareDistance(this in Vector3 a, Vector3 b)
    {
        Vector2 aInVec2 = new Vector2(a.x, a.z);
        Vector2 bInVec2 = new Vector2(b.x, b.z);

        return Vector2.Distance(aInVec2, bInVec2);
    }
}
