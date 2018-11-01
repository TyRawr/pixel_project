using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGoo : Enemy
{
    public Transform m_GroundCheck;
    public Animator animator;
    public Rigidbody2D m_Rigidbody2D;

    bool jump;
    bool m_Grounded;
    float jumpCooldown = 2f;
    float timeSinceLastJump = 0f;
    [SerializeField] private float m_HorizontalForce = 50f;
    [SerializeField] private float m_JumpForce = 300f;
    [SerializeField] private LayerMask m_WhatIsGround;

    const float k_GroundedRadius = .001f;

    public virtual void ToggleHorizontalForce()
    {
        m_HorizontalForce *= -1;
    } 

    protected override void WakeUp()
    {
        base.WakeUp();
        active = true;
    }

    private void Awake()
    {
        
    }

    [SerializeField] private float timeToWake = .3f; // seconda
    // Use this for initialization
    protected override void Start()
    {
        if(timeToWake > 0f)
        {
            active = false;
            SleepForTime(timeToWake);
        } else
        {
            Jump();
        }
    }

    private bool active = true;
    // Update is called once per frame
    protected override void Update()
    {
        float dt = Time.deltaTime;
        if (!active) return; // take no action
        timeSinceLastJump += dt;

        if (timeSinceLastJump > jumpCooldown)
        {
            timeSinceLastJump = 0f;
            Jump();
            return;
        } else
        {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                    {
                        animator.SetBool("isJumping", false);
                    }
                }
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(jump)
        {
            jump = false;
            m_Grounded = false;
            m_Rigidbody2D.transform.position = (new Vector2(m_Rigidbody2D.position.x, m_Rigidbody2D.position.y + 0.01f));
            m_Rigidbody2D.AddForce(new Vector2(m_HorizontalForce, m_JumpForce));
        }
        float verticalVelocity = m_Rigidbody2D.velocity.y;
        //Debug.Log("verical velocity " + verticalVelocity);
        animator.SetFloat("verticalSpeed", verticalVelocity);
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);
    }

    protected override void Die()
    {
        base.Die();
    }

    private void Jump()
    {
        jump = true; //tells fixed update to jump
        animator.SetBool("isJumping", jump);
    }
}
