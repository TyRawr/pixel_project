using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int weaponDamage = 1;
    public float speed = 10f;
    public Rigidbody2D rigidbody2D;
	// Use this for initialization
	void Start () {
        //rigidbody2D.velocity = transform.right * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.gameObject.name);
        Destroy(gameObject);
        var enemy = hitInfo.transform.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(weaponDamage);
        }
    }
}
