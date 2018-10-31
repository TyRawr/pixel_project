using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float gravity = 40f;
    public float jumpSpeed = 15f;

    

    // private
    // hover
    bool wasHovering = false;
    bool stopHover = false;
    bool canHover = true;

    // movement and gravity
    const float k_GroundedStickingVelocityMultiplier = 1f;
    float horizontalMove = 0f;
    float m_VerticalGravityModifier;
    Vector2 m_MoveVector = Vector2.zero;

    // Use this for initialization
    void Start () {
		
	}

    
    void StopHover()
    {
        stopHover = true;
        wasHovering = false;
        CancelInvoke("StopHover");
    }


	// Update is called once per frame
	void Update () {
        //jump
        UpdateJump();
        //hover
        UpdateHover();
        // movement
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float horzAxisRaw = Input.GetAxisRaw("Horizontal");
        bool horzInputGiven = Mathf.Abs(horzAxisRaw) > 0;
        horizontalMove = horzInputGiven ? Mathf.Sign(horzAxisRaw) * runSpeed : 0;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        // grounded Horizontal Movement
        m_MoveVector.x = Mathf.MoveTowards(m_MoveVector.x, horizontalMove, 100 /* ground accel/decel */ * Time.deltaTime);

        m_MoveVector.y -= gravity * Time.deltaTime * m_VerticalGravityModifier;

        if (controller.Grounded)
        {
            if (m_MoveVector.y < -gravity * Time.deltaTime * k_GroundedStickingVelocityMultiplier)
            {
                m_MoveVector.y = -gravity * Time.deltaTime * k_GroundedStickingVelocityMultiplier;
            }
        }
    }

    private bool UpdateJump()
    {
        if (Input.GetButtonDown("Jump") && controller.Grounded) // maybe double jump logic
        {
            animator.SetBool("isJumping", true);
            Debug.Log("Set Vertical Movement " + jumpSpeed);
            StopHover();
            SetVerticalMovement(jumpSpeed);
            return true;
        }
        return false;
    }

    private void UpdateHover()
    {
        // hover logic
        bool hoverInputKeyDown = Util.GetAxisInputLikeOnKeyDown("Fire2");// Input.GetAxis("Fire2") > 0;
        bool hoverInputKeyUp = Util.GetAxisInputLikeOnKeyUp("Fire2");
        bool hoverInput = Input.GetAxis("Fire2") > 0.5f;

        //cancel hover
        if (hoverInputKeyUp)
        {
            StopHover();
            canHover = true;
        }

        //start hover
        if (hoverInputKeyDown && canHover)
        {
           
            Invoke("StopHover", 2f); // hover should not last longer than 1 second
            wasHovering = true; // true for this frame
            canHover = false;
            Debug.Log("St Hover " + 1);
            SetVerticalMovement(.5f);
        }
        if (wasHovering)
        {
            m_VerticalGravityModifier = 0f;
        } else
        {
            m_VerticalGravityModifier = 1f;
        }
        // end hover logic
    }



    private void FixedUpdate()
    {
        controller.Move(m_MoveVector * Time.fixedDeltaTime);
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void SetVerticalMovement(float newVerticalMovement)
    {
        m_MoveVector.y = newVerticalMovement;
    }

    public void SetHorizontalMovement(float newHorizontalMovement)
    {
        m_MoveVector.x = newHorizontalMovement;
    }

    public void AddToMovement(Vector2 additionalMovement)
    {
        m_MoveVector += additionalMovement;
    }

    public void AddToHorizontalMovement(float additionalHorizontalMovement)
    {
        m_MoveVector.x += additionalHorizontalMovement;
    }

    public void AddToVertialMovement(float additionalVerticalMovement)
    {
        m_MoveVector.y += additionalVerticalMovement;
    }
}
