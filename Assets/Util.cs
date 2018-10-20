using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {

    public static Transform Find(string name)
    {
        return GameObject.Find(name).transform;
    }

    public static Transform FindPlayer() // could be turned into find NEAREST player
    {
        return GameObject.Find("Ty").transform;
    }

    public static Quaternion LookAt2D(Transform self, Transform target)
    {
        Vector2 selfVec2D = new Vector2(self.position.x, self.position.y);
        Vector2 targetVec2D = new Vector2(target.position.x, target.position.y);
        Vector2 diff = targetVec2D - selfVec2D;
        diff.Normalize();
        float zRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, zRot - 180f);
    }

}
