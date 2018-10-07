using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public GameObject impactEffect;

    public int weaponDamage = 1;
    public CharacterController2D characterController2D;
    // Use this for initialization
    void Start()
    {

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
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void ShootBullet()
    {
        GameObject b = Instantiate(bullet, firePoint.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 20f;
    }
}
