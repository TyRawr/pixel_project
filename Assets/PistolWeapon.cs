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
    public float cooldown = .1f; // time in seconds between each fire, this is an automatic weapon
    public float bulletSpeed = 20f;
    private float cooldownTimer = 0f;
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
        updateAndCheckTime();
        if (_canFire && (InputManager.GetButton("Fire1", characterController2D) || InputManager.GetAxis("Fire1", characterController2D) > 0)  )
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
        _canFire = false;
        cooldownTimer = 0f;
        GameObject b = Instantiate(bullet, firePoint.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Bullet bul = b.GetComponent<Bullet>();
        bul.SetTransformThatWasShotFrom(transform);
    }


    ////


    bool _canFire = true;
    bool updateAndCheckTime()
    {
        cooldownTimer += Time.deltaTime;
        float progress = Mathf.Clamp(cooldown - cooldownTimer, 0, 1);
        //Debug.Log(progress);
        Util.SetUISliderValue(1 - progress);
        if (cooldownTimer >= cooldown )
        {
            _canFire = true;
        }
        return _canFire;
    }
}
