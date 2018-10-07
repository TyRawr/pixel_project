using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 3;
    public GameObject deathParticleSystem;
    // Use this for initialization
    protected virtual void Start () {
		
	}

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected virtual void FixedUpdate()
    {

    }

    public virtual void TakeDamage(int damageTaken)
    {
        this.health -= damageTaken;
        if(this.health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if(deathParticleSystem)
        {
            GameObject.Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        }

        GameObject.Destroy(this.gameObject);
    }
}
