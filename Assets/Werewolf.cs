using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : MonoBehaviour {

    public Transform player;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Vector2 m_MoveVector = Vector2.zero;
    private Vector2 m_CurrentPosition, m_PreviousPosition;
    private Vector2 m_NextMovement;
    private CharacterController2D controller;

	// Use this for initialization
	void Start () {
        player = Util.FindPlayer();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        m_CurrentPosition = m_PreviousPosition = transform.position;

    }
	
    enum State
    {
        Idle,
        Chasing,
        Attacking
    }



	// Update is called once per frame
	void Update () {
        Vector2 gravity = Physics2D.gravity;
        Vector2 p = transform.position;
        Vector2 q = player.position;
        Vector2 d =  p - q;
        Debug.Log("d" + d + "  +++ d.magnitude " + d.magnitude);
        //m_MoveVector.y = Mathf.MoveTowards(m_MoveVector.y, -gravity.y, 50 /* ground accel/decel */ * Time.deltaTime);
        // update action
        bool currentlyChasing = animator.GetBool("Chasing");
        // possible raycast here to see if inbetween walls -> ray should be uninhibited if cast only against player layer
        if (d.magnitude < 7 && !currentlyChasing)
        {
            //Deb
            Debug.LogWarning("Can Chase");
            animator.SetBool("Chasing", true);
            currentlyChasing = true;
            Transform player = Util.Player;
            //x direction
            Vector2 playerPosition = player.position;
            Vector2 position = transform.position;
            Vector2 diff = playerPosition - position;
            Debug.LogWarning("diff " + diff);

        }
        if (d.magnitude >= 8 && currentlyChasing)
        {
            Debug.LogWarning("Stop Chase - too far away");
            animator.SetBool("Chasing", false);
        }
        if(d.magnitude < 2)
        {
            animator.SetTrigger("Attacking");
        }
        if(d.magnitude < 7 && currentlyChasing)
        {
            float runSpeed = 10f;
            
            int direction = -1;
            //rigidbody.MoveTow
            //rigidbody.MovePosition(new Vector2(direction * runSpeed * Time.deltaTime, gravity.y));
            //m_NextMovement.x = Mathf.MoveTowards(m_CurrentPosition.x, runSpeed, 50 /* ground accel/decel */ * Time.deltaTime);
            
        }
        Move();
    }

    private void Move()
    {
        m_CurrentPosition = m_PreviousPosition + m_NextMovement;

        rigidbody.MovePosition(new Vector2(m_CurrentPosition.x, m_CurrentPosition.y));
        m_NextMovement = Vector2.zero;
    }
}
