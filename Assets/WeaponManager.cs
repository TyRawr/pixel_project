using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public List<GameObject> weapons;
    private int _active_index;
    [SerializeField]
    CharacterController2D controller;
	// Use this for initialization
	void Start () {
		
	}
	
    void SetWeaponAtIndexActive(int index)
    {
        _active_index = index;
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
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAllWeaponsInactive();
            SetWeaponAtIndexActive(1 - 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAllWeaponsInactive();
            SetWeaponAtIndexActive(2 - 1);
        }
        if (InputManager.GetButtonDown("WeaponSwap", controller))
        {
            int newIndex = ToggleWeaponIndex(_active_index) ;
            Debug.Log("WeaponSwap " + newIndex);
            SetAllWeaponsInactive();
            SetWeaponAtIndexActive(newIndex);
        }
    }

    int ToggleWeaponIndex(int currentIndex)
    {
        // too high to math this
        if(currentIndex == 0)
        {
            return 1;
        }
        return 0;
    }
}
