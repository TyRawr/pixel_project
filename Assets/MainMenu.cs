using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update () {

	}
}
