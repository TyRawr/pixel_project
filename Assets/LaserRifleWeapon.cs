using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRifleWeapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject impactEffect;
    // TODO: do the line stuff!
    public LineRenderer lineRenderer;
    public int weaponDamage = 1;
    public float cooldown = .1f; // time in seconds between each fire, this is an automatic weapon
    private float cooldownTimer = 0f;
    // Use this for initialization
    public CharacterController2D characterController2D;

    private GameObject impactEffectClone;

    // Use this for initialization
    void Start()
    {
        lineRenderer.enabled = false;
    }

    private void Awake()
    {
        characterController2D.arm = this.transform;
    }

    private void OnEnable()
    {
        characterController2D.arm = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") || Input.GetAxis("Fire1") > 0)
        {
            if(updateAndCheckTime())
            {
                Shoot();
            }
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }

    bool updateAndCheckTime()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer > cooldown - Time.deltaTime)
        {
            //Debug.Log("cooldownTimer " + cooldownTimer);
            cooldownTimer = 0f; // reset the cooldown timer
            return true;
        }
        return false;
    }


    bool updateAndCheckFixedTime()
    {
        cooldownTimer += Time.fixedDeltaTime;
        if (cooldownTimer > cooldown)
        {
            Debug.Log("cooldownTimer " + cooldownTimer);
            cooldownTimer = 0f; // reset the cooldown timer
            return true;
        }
        return false;
    }
    [SerializeField] private LayerMask m_HitMask;

    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 1000f, m_HitMask);
        
        bool hitFoundAndNotPlayer = hitInfo && hitInfo.rigidbody && !(hitInfo.rigidbody.gameObject.layer == LayerMask.NameToLayer("Player"));
        if(hitFoundAndNotPlayer)
        {

            if (impactEffectClone)
            {
                Destroy(impactEffectClone); // only one impact at a time
            }
            impactEffectClone = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            
            Debug.Log("hit " + hitInfo.collider.gameObject.name);
            Vector2 hit_point = hitInfo.point;
            var enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
            if(enemy)
            {
                DPSObserver.DamageEntry(weaponDamage);
                enemy.TakeDamage(weaponDamage);
            } else
            {
                //Debug.Log(hitInfo.rigidbody.name);
            }

            StartCoroutine(RunImpactEffect(impactEffectClone));
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit_point);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100f);
        }
        lineRenderer.enabled = true;
        StartCoroutine(RunLineRenderer());
    }

    IEnumerator RunLineRenderer()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        lineRenderer.enabled = false;
    }

    IEnumerator RunImpactEffect(GameObject thatGameObject)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (thatGameObject)
        {
            thatGameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Destroy(thatGameObject);
        }
    }
}
