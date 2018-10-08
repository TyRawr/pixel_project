using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}


    public Camera playerCamera;
    private Vector2 worldMousePos;
	// Update is called once per frame
	void Update () {
        float armRotationX = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
        bool faceRight = armRotationX >= 0f;
        if (faceRight)
        {
            spriteRenderer.flipY = false;
        } else
        {
            spriteRenderer.flipY = true;
        }
    }

    Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0f, 0f, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private void FixedUpdate()
    {
        
        Vector3 diff = GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
