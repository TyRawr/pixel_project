using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    protected int health = 3;
    [SerializeField]
    protected GameObject deathParticleSystem;
    // Use this for initialization
    protected virtual void Start () {
		
	}

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected virtual void WakeUp()
    {
        // probs just wanna override this
    }

    protected virtual void SleepForTime(float seconds)
    {
        Invoke("WakeUp", seconds);
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
