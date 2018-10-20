using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    Rigidbody2D rb;

    float period = 30f;
    float radius = 4f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        float y = Mathf.Sin(Time.time);
        float x = Mathf.Cos(period * Time.time / period);
        Debug.Log("y " + y);
        Debug.Log(radius * y);
        rb.MovePosition(rb.position - new Vector2(.03f, y / 30));
	}
}
