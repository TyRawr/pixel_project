using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEyeEnemy : Enemy {

    Transform target;
    private Vector3 attackPosition;
    private Vector3 startPos;

    float amplitude = 10f;
    float period = 5f;

    private bool attacking;
    private Rigidbody2D rb;

    [SerializeField]
    private float lerpTime = 3f;

    private float currentLerpTime = 0f;

    public void Awake()
    {
        attacking = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    protected override void Start () {
        InvokeRepeating("AttackPlayer", 3f, 3f);
        
    }

    // Update is called once per frame
    protected override void Update () {
        if (target)
            transform.rotation = Util.LookAt2D(transform, target);
	}

    protected override void FixedUpdate()
    {
        if (attacking)
        {
            // simply move toward the targetPosition
            Vector3 newPos = Vector3.zero;
            currentLerpTime += Time.fixedDeltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
                attacking = false;
            }

            float perc = currentLerpTime / lerpTime;
            newPos = Vector3.Lerp(startPos, attackPosition, perc);
            //newPos = Vector3.Slerp(transform.position, attackPosition, diveAttackSpeed * Time.fixedDeltaTime);
            rb.MovePosition(new Vector2(newPos.x, newPos.y));
        }
    }

    private void AttackPlayer()
    {
        FindTarget();
        SetDiveAttackLocation();
        DiveAttack();
    }

    void DiveAttack()
    {
        Debug.Log("attacking");
        currentLerpTime = 0f;
        startPos = transform.position;
        attackPosition = target.position;
        attacking = true;
    }

    //possibly put SetTarget in a util class (static?)

    private void FindTarget()
    {
        target = Util.FindPlayer();
    }

    // probably dont call this every frame
    private void SetDiveAttackLocation()
    {
        attackPosition = target.position;
    }
}
