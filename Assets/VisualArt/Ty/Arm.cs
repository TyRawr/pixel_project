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



    private void FixedUpdate()
    {
        float rot_z = Util.GetArmAngle(transform);
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
