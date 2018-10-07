using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLaserStatic : MonoBehaviour {

    public float initialFireDelay = 0f;
    public float fireTime = .5f;
    public float sleepTime = 3f;

    Animator leftGeneratorAnimator;
    Animator rightGeneratorAnimator;
    Animator middleAnimator;
    GameObject leftGenerator;
    GameObject rightGenerator;
    GameObject middle;
    SpriteRenderer middleSpriteRenderer;
    BoxCollider2D boxCollider2D;
    float widthOfGreenLaser;

    // Use this for initialization
    void Start () {
        leftGenerator = transform.Find("LeftGenerator").gameObject;
        rightGenerator = transform.Find("RightGenerator").gameObject;
        middle = transform.Find("Middle").gameObject;
        rightGeneratorAnimator = rightGenerator.GetComponent<Animator>();
        leftGeneratorAnimator = leftGenerator.GetComponent<Animator>();
        middleAnimator = middle.GetComponent<Animator>();
        middleSpriteRenderer = middle.GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.offset = Vector2.zero;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        boxCollider2D.size = new Vector2(widthOfGreenLaser = rectTrans.rect.width, rectTrans.rect.height);
        StartCoroutine(FireTimer());
    }

    void Fire()
    {
        leftGeneratorAnimator.SetBool("Fire", true);
        rightGeneratorAnimator.SetBool("Fire", true);
        middleAnimator.SetBool("Fire", true);
        boxCollider2D.enabled = true;
    }

    void StopFire()
    {
        leftGeneratorAnimator.SetBool("Fire", false);
        rightGeneratorAnimator.SetBool("Fire", false);
        middleAnimator.SetBool("Fire", false);
        boxCollider2D.enabled = false;
    }

    void ToggleFiring()
    {
        leftGeneratorAnimator.SetBool("Fire", !leftGeneratorAnimator.GetBool("Fire"));
        rightGeneratorAnimator.SetBool("Fire", !rightGeneratorAnimator.GetBool("Fire"));
        middleAnimator.SetBool("Fire", !middleAnimator.GetBool("Fire"));
        boxCollider2D.enabled = middleAnimator.GetBool("Fire");
    }

    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(initialFireDelay);
        while (true)
        {
            Fire(); 
            yield return new WaitForSeconds(fireTime); // will be firing for this time
            StopFire();
            yield return new WaitForSeconds(sleepTime);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update () {
        middleSpriteRenderer.size = new Vector2(widthOfGreenLaser - 1f, 0.6875f); //0.6875f is a hard coded value that matches the green line rather than the whole container for the laser

        if (Input.GetKeyDown(KeyCode.C))
        {
            leftGeneratorAnimator.SetBool("Fire", !leftGeneratorAnimator.GetBool("Fire"));
            rightGeneratorAnimator.SetBool("Fire", !rightGeneratorAnimator.GetBool("Fire"));
            middleAnimator.SetBool("Fire", !middleAnimator.GetBool("Fire"));
        }
	}
}
