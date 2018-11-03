using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int weaponDamage = 1;
    public float speed = 10f;

    [SerializeField] LayerMask m_CollideWith;// = LayerMask.GetMask("Platform", "Enemy");
    //public Vector3 rigidbody2D;
    private Transform weaponTransform;

    private Vector2 m_LastPosition;
	// Use this for initialization
	void Start () {
        //rigidbody2D.velocity = transform.right * speed;
        m_LastPosition = transform.position;
	}
	public void SetTransformThatWasShotFrom(Transform t)
    {
        weaponTransform = t;
    }
    // Update is called once per frame
    void FixedUpdate () {

        Vector2 currentPosition =  transform.position;
        Vector2 direction = currentPosition - m_LastPosition;
        direction.Normalize();

        if(m_LastPosition.x == currentPosition.x && m_LastPosition.y == currentPosition.y)
        {
            direction = weaponTransform.right;
        }

        
        RaycastHit2D hitInfo = Physics2D.Raycast(m_LastPosition - direction, direction, 1f, LayerMask.GetMask("Platform"));
        
        bool hitFoundAndNotPlayer = hitInfo && hitInfo.rigidbody &&
            hitInfo.rigidbody.gameObject != this.gameObject
            && !(hitInfo.rigidbody.gameObject.layer == LayerMask.NameToLayer("Player"));
        if(hitInfo)
        {
            Debug.Log(hitInfo.rigidbody.gameObject.name);
        }
        if (hitFoundAndNotPlayer)
        {
            Debug.LogError("fucked shit stacked");
            GetComponent<ExplodeOnImpact>().DoExplode();
            
            GameObject.Destroy(gameObject);
            return;
        }
        Debug.DrawRay(m_LastPosition - direction, direction, Color.magenta, .1f);

        m_LastPosition = currentPosition;
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
