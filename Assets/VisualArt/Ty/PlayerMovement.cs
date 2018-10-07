using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            jump = true;
            animator.SetBool("isJumping", jump);
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalMove * Time.deltaTime, 0f);
        controller.Move(movement.x, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
}
