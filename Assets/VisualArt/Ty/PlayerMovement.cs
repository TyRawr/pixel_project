using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float gravity = 40f;
    public float jumpSpeed = 15f;

    protected const float k_GroundedStickingVelocityMultiplier = 3f;

    float horizontalMove = 0f;
    bool jump;

    Vector2 m_MoveVector = Vector2.zero;

    // Use this for initialization
    void Start () {
		
	}

    bool wasHovering = false;
    bool stopHover = false;
    bool hoverInputWasDown = false;
    bool canHover = true;
    void StopHover()
    {
        Debug.Log("stop hover");
        stopHover = true;
        wasHovering = false;
    }


	// Update is called once per frame
	void Update () {
        float horzAxisRaw = Input.GetAxisRaw("Horizontal");
        bool horzInputGiven = Mathf.Abs(horzAxisRaw) > 0;
        horizontalMove = horzInputGiven ? Mathf.Sign(horzAxisRaw) * runSpeed : 0;


        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump") && controller.Grounded) // maybe double jump logic
        {
            Debug.Log("Jump");
            //jump = true;
            animator.SetBool("isJumping", true);
            SetVerticalMovement(jumpSpeed);
        }

        // hover logic
        float verticalGravityMultiplier = 1f;
        
        bool hoverInputKeyDown = Util.GetAxisInputLikeOnKeyDown("Fire2");// Input.GetAxis("Fire2") > 0;
        bool hoverInputKeyUp = Util.GetAxisInputLikeOnKeyUp("Fire2");
        bool hoverInput = Input.GetAxis("Fire2") > 0.1f;

        //cancel hover
        if(hoverInputKeyUp)
        {
            Debug.Log("hoverInputKeyUp");
            StopHover();
            canHover = true;
        }

        //start hover
        if (hoverInputKeyDown && canHover)
        {
            Invoke("StopHover", 1f); // hover should not last longer than 1 second
            wasHovering = true; // true for this frame
            canHover = false;
            SetVerticalMovement(0f);
        }
        if(wasHovering)
        {
            verticalGravityMultiplier = 0f;
        }
        
        // end hover logic


        // grounded Horizontal Movement
        m_MoveVector.x = Mathf.MoveTowards(m_MoveVector.x, horizontalMove, 100 /* ground accel/decel */ * Time.deltaTime);

        m_MoveVector.y -= gravity * Time.deltaTime * verticalGravityMultiplier;

        if(controller.Grounded)
        {
            if (m_MoveVector.y < -gravity * Time.deltaTime * k_GroundedStickingVelocityMultiplier)
            {
                m_MoveVector.y = -gravity * Time.deltaTime * k_GroundedStickingVelocityMultiplier;
            }
        }
        
    }



    private void FixedUpdate()
    {
        //Vector2 movement = new Vector2(horizontalMove * Time.deltaTime, 0f);
        //Debug.Log("move via input: " + movement);
        controller.Move(m_MoveVector * Time.fixedDeltaTime);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void SetVerticalMovement(float newVerticalMovement)
    {
        m_MoveVector.y = newVerticalMovement;
    }
}
