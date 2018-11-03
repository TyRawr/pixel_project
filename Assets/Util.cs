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

    private static bool _rightStickInputActive = false;
    public static bool RightStickInputActive
    {
        get { return _rightStickInputActive;  }
    }

    private static Vector2 _oldRightJoystickInputVector;
    public static float FindNearestEnemyToLine(Transform self)
    {
        MultipleTargetCamera mtc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MultipleTargetCamera>();
        string[] joystickNames = Input.GetJoystickNames();
        HashSet<string> joystickNamesList = new HashSet<string>(joystickNames);
        joystickNamesList.Remove("");
        if (joystickNamesList != null && joystickNamesList.Count > 0)
        {
            float horizontal = Input.GetAxis("JoystickHorizontal");
            float vertical = Input.GetAxis("JoystickVertical");
            

            Vector3 rayOrigin = self.position;
            Vector3 rayDirection = new Vector3(horizontal, vertical, 0f);
            rayDirection.Normalize();
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            Enemy[] enemies = new Enemy[enemyObjects.Length];
            Vector3 vectToEnemy = Vector3.negativeInfinity;
            float smallestDistance = float.PositiveInfinity;
            for (int i = 0; i < enemyObjects.Length; i++)
            {
                Enemy e = enemies[i] = enemyObjects[i].GetComponent<Enemy>();

                Vector3 point = e.transform.position;

                Vector3 vectorToEnemy = (point - self.position);
                vectorToEnemy.Normalize();

                Vector3 proj = Vector3.Project(vectorToEnemy, rayDirection);
                //Debug.Log("proj: " + proj);

                float dot = Vector3.Dot(rayDirection, vectorToEnemy);
                Debug.DrawRay(rayOrigin, 10 * rayDirection, Color.yellow);
                //Debug.Log("dot " + Vector3.Dot(rayDirection, vectorToEnemy));
                if (dot > .95)
                {
                    //snap to target
                    float dToEnemy = Mathf.Abs(e.transform.position.x - self.position.x);
                    //Debug.Log("dist to enemy: " + dToEnemy);
                    float distance = Vector3.Distance(rayOrigin, point);
                    float angle = Vector3.Angle(rayDirection, point - rayOrigin);
                    float dist = (distance * Mathf.Sin(angle * Mathf.Deg2Rad));
                    //Debug.Log("dist " + dist);
                    if (dist < .5f && dToEnemy < 12 && dot * dToEnemy < smallestDistance)
                    {
                        vectToEnemy = vectorToEnemy;
                        smallestDistance = dToEnemy;
                    }
                }
                

            }
            if(enemyObjects.Length == 0)
            {
                
            }
            _rightStickInputActive = Mathf.Abs(vertical) > 0.1f || Mathf.Abs(horizontal) > 0.1f;
            if (_rightStickInputActive)
            {
                _oldRightJoystickInputVector = new Vector2(horizontal, vertical);
                _oldRightJoystickInputVector.Normalize(); 
            }
            mtc.offset = new Vector3 (_oldRightJoystickInputVector.x, _oldRightJoystickInputVector.y, -8);
            if (vectToEnemy.z > -10f)
            {
                //something was set

                return Mathf.Atan2(vectToEnemy.y, vectToEnemy.x) * Mathf.Rad2Deg;
            }
            mtc.offset = new Vector3(_oldRightJoystickInputVector.x, _oldRightJoystickInputVector.y, -8);
            return Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        } else
        {
            Vector3 diff = GetWorldPositionOnPlane(Input.mousePosition, 0) - self.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            return rot_z;
        }
        mtc.offset = new Vector3(_oldRightJoystickInputVector.x, _oldRightJoystickInputVector.y, -8);
        _rightStickInputActive = false;
        return 0f;
    }

    public static float GetArmAngle(Transform transform)
    {
        string[] joystickNames = Input.GetJoystickNames();
        if(joystickNames != null && joystickNames[0] != string.Empty)
        {
            float horizontal = Input.GetAxis("JoystickHorizontal");
            float vertical = Input.GetAxis("JoystickVertical");

            _rightStickInputActive = Mathf.Abs(vertical) > 0.1f || Mathf.Abs(horizontal) > 0.1f;
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
