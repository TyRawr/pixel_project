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

    private static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0f, 0f, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public static float GetArmAngle(Transform transform)
    {
        string[] joystickNames = Input.GetJoystickNames();
        if(joystickNames != null && joystickNames[0] != string.Empty)
        {
            float horizontal = Input.GetAxis("JoystickHorizontal");
            //Debug.Log("horizontal " + horizontal);
            float vertical = Input.GetAxis("JoystickVertical");
            //Debug.Log("vertical " + vertical);
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            return angle;
        } else
        {
            Vector3 diff = GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            return rot_z;
        }
    }

    static bool _lastInputAxisState; // true == down
    public static bool GetAxisInputLikeOnKeyDown(string axisName)
    {
        var currentInputValue = Input.GetAxis(axisName) > 0.1;

        // prevent keep returning true when axis still pressed.
        if (currentInputValue && _lastInputAxisState)
        {
            return false;
        }

        _lastInputAxisState = currentInputValue;

        return currentInputValue;
    }

    static bool lastAxisForKeyUp = false;
    public static bool GetAxisInputLikeOnKeyUp(string axisName)
    {
        var currentInputValue = Input.GetAxis(axisName) <= 0.1;

        if(!currentInputValue)
        {
            lastAxisForKeyUp = true;
        }
        // prevent keep returning true when axis still pressed.
        if (currentInputValue && lastAxisForKeyUp)
        {
            lastAxisForKeyUp = false;
            return true;
        }

        return false;
    }
}
