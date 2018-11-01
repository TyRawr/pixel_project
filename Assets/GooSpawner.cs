using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooSpawner : EnemySpawner {

    [SerializeField] protected bool startDirectionRight = true;

    protected override void Spawn()
    {
        GameObject item = GameObject.Instantiate(m_ObjectToSpawn, transform, false);
        item.transform.position = transform.position;
        item.SetActive(true);
        if(!startDirectionRight)
        {
            MiniGoo miniGoo = item.GetComponent<MiniGoo>();
            miniGoo.ToggleHorizontalForce();
        }
    }
}
