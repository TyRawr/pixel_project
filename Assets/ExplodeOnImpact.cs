using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour {
    [SerializeField] float radius = 3f;
    [SerializeField] LayerMask whatToHit;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] int damage = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Explosion collision do explode with radius " + radius + " collided with " + collider.name);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, whatToHit);
        foreach(Collider2D c in colliders)
        {
            Debug.Log("nuke " + c.name);
            Enemy e = c.GetComponent<Enemy>();
            e.TakeDamage(damage);
            
        }
        GameObject.Instantiate(explosionEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Explosion collision do explode with radius " + radius + " collided with " + collision.collider.name);
    }
}
