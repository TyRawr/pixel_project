using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPSObserver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Note: weapon dps is the weapon damage / cooldown fields
    // E.g. wp dmg = 1 and cooldown = 1 is 1 dps
    // E.g. wp dmg 100 and cooldown = 1 is 100 dps;
    // E.g. wp dmg 1 and cooldown .01 is 100 dps; 


    static public float timer = 1f;
    static float timeMarker = 0f; // dis tin gone go up to clock
    static float startTime = Mathf.NegativeInfinity;
    static float endTime = Mathf.Infinity;
    static float damageTotal = 0f;

                           // Update is called once per frame
    void Update () {
        if (!init) return;
        timeMarker += Time.deltaTime;

        if (timeMarker > timer)
        {
            // fire timer fired event
            endTime = Time.time;
            float timeDiff = endTime - startTime;
            Debug.Log("totalDamage " + damageTotal);
            Debug.Log("timeDiff " + timeDiff);
            float damageNormalized = damageTotal / timeDiff;
            Debug.Log("damageNormalized DPS " + damageNormalized);
            Clear();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Clear");
            this.Clear();
        }

        float dt = Time.deltaTime;
        float dps = damageTotal / dt;
        //Debug.Log("dps " + dps);
        //damageTotal = 0;
	}

    void Clear()
    {
        timeMarker = 0f;
        startTime = Mathf.NegativeInfinity;
        endTime = Mathf.Infinity;
        damageTotal = 0;
        init = false;
    }
    
    static bool init = false;
    static void Init()
    {
        if (init) return;
        init = true;
        damageTotal = 0;
        timeMarker = 0f;
        endTime = Mathf.Infinity;

        //update timer
        startTime = Time.time;
    }

    public static void DamageEntry(float damageDealt, GameObject from = null)
    {
        if(!init)
        {
            Init();
        }
        DPSObserver.damageTotal += damageDealt;
        //from -> log later
    }
}
