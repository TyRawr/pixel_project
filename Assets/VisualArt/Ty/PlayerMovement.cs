using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public CharacterController2D controller;
    public GameObject m_HoverEffect;
    public float runSpeed = 40f;
    public float gravity = 40f;
    public float jumpSpeed = 15f;
    public float m_HoverResource = 100f;



    // private
    // hover
    public bool wasHovering = false;
    public bool stopHover = false;
    public bool canHover = true;

    // movement and gravity
    const float k_GroundedStickingVelocityMultiplier = 1f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float m_VerticalGravityModifier;
    Vector2 m_MoveVector = Vector2.zero;


    float m_HorizontalAxisRaw;
    float m_VerticalAxisRaw;
    bool m_HorizontalInputGiven;
    bool m_VerticalInputGiven;
    bool m_HoverInputKeyDown;
    bool m_HoverInputKeyUp;
    bool m_HoverInput;
    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        SetInput();
        UpdateJump();
        UpdateMovement();
        UpdateHover();
    }

    private void SetInput()
    {
        m_VerticalAxisRaw = InputManager.GetAxisRaw("Vertical", controller);
        m_HorizontalAxisRaw = InputManager.GetAxisRaw("Horizontal", controller);
        m_VerticalInputGiven = Mathf.Abs(m_VerticalAxisRaw) > .1;
        m_HorizontalInputGiven = Mathf.Abs(m_HorizontalAxisRaw) > 0.1;
        m_HoverInputKeyDown = Util.GetAxisInputLikeOnKeyDown("Fire2", controller);// InputManager.GetAxis("Fire2") > 0;
        m_HoverInputKeyUp = Util.GetAxisInputLikeOnKeyUp("Fire2",controller);
        m_HoverInput = InputManager.GetAxis("Fire2", controller) > 0.5f;
    }

    private void UpdateHover()
    {
        if(m_HoverInputKeyUp)
        {
            m_VerticalGravityModifier = 1;
            m_HoverEffect.SetActive(false);
            
        }
        if(m_HoverInput)
        {
            if(m_HoverInputKeyDown)
            {
                m_VerticalGravityModifier = 0;
                m_HoverEffect.SetActive(true);
                Vector2 temp = m_MoveVector;
                SetVerticalMovement(0);
            }
        }
    }

    private void UpdateMovement()
    {
        if(m_HoverInput /* and have resource and ability to use hover */)
        {
            horizontalMove = m_HorizontalInputGiven ? Mathf.Sign(m_HorizontalAxisRaw) * runSpeed / 2f : 0;
            verticalMove = m_VerticalInputGiven ? Mathf.Sign(m_VerticalAxisRaw) * runSpeed / 2f * -1f : 0;
            m_MoveVector.x = Mathf.MoveTowards(m_MoveVector.x, horizontalMove, 50 /* ground accel/decel */ * Time.deltaTime);
            m_MoveVector.y = Mathf.MoveTowards(m_MoveVector.y, verticalMove, 50 /* ground accel/decel */ * Time.deltaTime);
        }
        else
        {
            horizontalMove = m_HorizontalInputGiven ? Mathf.Sign(m_HorizontalAxisRaw) * runSpeed : 0;
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
    }

    private bool UpdateJump()
    {
        if (InputManager.GetButtonDown("Jump", controller) && controller.Grounded) // maybe double jump logic
        {
            animator.SetBool("isJumping", true);
            //Debug.Log("Set Vertical Movement " + jumpSpeed);
            SetVerticalMovement(jumpSpeed);
            return true;
        }
        return false;
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
