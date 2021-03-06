﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Util.JoystickEnabled();
        player = transform.parent.parent.GetComponent<CharacterController2D>();
    }

    public Camera playerCamera;
    private Vector2 worldMousePos;
    private CharacterController2D player;
	// Update is called once per frame
	void Update () {
        float armRotationX = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
        bool faceRight = armRotationX >= 0f;
        //Debug.Log(Util.UsingJoystick);
        if(Util.UsingJoystick && Util.RightStickInputActive)
        {
            
            if (faceRight)
            {
                spriteRenderer.flipY = false;
            }
            else
            {
                spriteRenderer.flipY = true;
            }
            
        }

        float rot_z = Util.FindNearestEnemyToLine(transform, player);
        //Debug.Log("rotz: " + rot_z);
        if (Util.UsingJoystick && Util.RightStickInputActive)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            Debug.DrawRay(transform.position, transform.right, Color.yellow);
        } else if(!Util.UsingJoystick)
        {
            if (faceRight)
            {
                spriteRenderer.flipY = false;
            }
            else
            {
                spriteRenderer.flipY = true;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        Util.FindNearestEnemyToLine(transform, player);
    }

}
