using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy {

    Transform target;
    bool attacking = false;
    private Vector3 attackPosition;

    private Rigidbody2D rb;
    const float k_GroundedRadius = .001f;
    bool m_Grounded;
    [SerializeField] private LayerMask m_WhatIsGround;
    public Transform m_GroundCheck;

    private bool attackOnCooldown = false;


    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    protected override void Start () {
        attacking = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        FindTarget();
        InvokeRepeating("AttackPlayer", 1f, 3f);
    }

    // Update is called once per frame
    protected override void Update () {
        float dt = Time.deltaTime;

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
                    //animator.SetBool("isJumping", false);
                }
            }
        }
        
    }

    protected override void FixedUpdate()
    {
        if (!m_Grounded) return;
        Vector3 newPos = Vector3.zero;
        if (attackOnCooldown) return;


        if (!attacking)
        {
            if (transform.position.x < target.position.x)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            spriteRenderer.color = Color.white;
            //newPos = Vector3.Lerp(transform.position, attackPosition, Time.fixedDeltaTime);
            newPos = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 1);
            rb.MovePosition(new Vector2(newPos.x, transform.position.y));
        } else
        {
            spriteRenderer.color = Color.red;
            newPos = Vector3.MoveTowards(transform.position, attackPosition, Time.deltaTime * 10);
            rb.MovePosition(new Vector2(newPos.x, transform.position.y));
            float f = Vector3.Distance(newPos, attackPosition);
            if(f < .03)
            {
                attacking = false;
                attackOnCooldown = true;
                StartCoroutine(WaitForCooldown());
                return;
            }
        }

    }

    IEnumerator WaitForCooldown()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(2f);
        attackOnCooldown = false;
    }

    private void FindTarget()
    {
        target = Util.FindPlayer();
    }

    private void AttackPlayer()
    {
        FindTarget();
        float distance = float.PositiveInfinity;
        if(target)
        {
            distance = Mathf.Abs(transform.position.x - target.position.x);
            if(distance > 10)
            {
                // nothing
                attacking = false;
            } else
            {
                DiveAttack();
                if (transform.position.x < target.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            
        }
    }

    void DiveAttack()
    {
        attackPosition = target.position;
        attacking = true;
    }
}
