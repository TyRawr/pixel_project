using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public MainMenu mainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("Start")))
        {
            Debug.Log("Main Menu");
            MainMenu.BringUpMenu(mainMenu);
        }
    }
}
