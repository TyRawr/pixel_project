using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] protected GameObject m_ObjectToSpawn;
    [SerializeField] protected float m_Interval = 1f; // 1 sec is reasonable
    [SerializeField] protected bool m_StartWarm = true; // spawns immeditately

	// Use this for initialization
	protected virtual void Start () {
        InvokeRepeating("Spawn", 0f, m_Interval);
        if(m_StartWarm)
        {
            Spawn();
        }
	}

    protected virtual void Spawn()
    {
        GameObject item = GameObject.Instantiate(m_ObjectToSpawn, transform, false);
        item.transform.position = transform.position;
        item.SetActive(true);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
