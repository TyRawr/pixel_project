using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotEye : MonoBehaviour {
    public GameObject player;
    float currentVelocity;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot_z -= 180;
        float currentZ = transform.eulerAngles.z;
        Debug.Log("currentZ " + currentZ);
        //float closeZ = Mathf.Round(rot_z);
        float closeZ = Mathf.SmoothDampAngle(currentZ, rot_z, ref currentVelocity, .5f);
        //closeZ = Mathf.Round(closeZ);
        transform.rotation = Quaternion.Euler(0f, 0f, closeZ);
    }
}
