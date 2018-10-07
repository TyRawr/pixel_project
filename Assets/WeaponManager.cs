using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public List<GameObject> weapons;

	// Use this for initialization
	void Start () {
		
	}
	
    void SetWeaponAtIndexActive(int index)
    {
        weapons[index].SetActive(true);
    }

    void SetAllWeaponsInactive()
    {
        foreach(GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }

	// handle weapon swapping
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAllWeaponsInactive();
            SetWeaponAtIndexActive(1 - 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAllWeaponsInactive();
            SetWeaponAtIndexActive(2 - 1);
        }
    }
}
