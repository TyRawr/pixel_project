using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is really a PlayerDamager
/// </summary>
public class Damager : MonoBehaviour {

    [SerializeField] private int m_Damage = 1;
    private LayerMask m_CausesDamageTo;
	// Use this for initialization
	void Start () {
        m_CausesDamageTo = LayerMask.NameToLayer("Player");
        Debug.Log(m_CausesDamageTo.value);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckCollider2D(Collider2D collider)
    {
        if (collider.gameObject.layer == m_CausesDamageTo)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            health.TakeDamage(m_Damage);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        CheckCollider2D(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        CheckCollider2D(collider); 
    }
}
