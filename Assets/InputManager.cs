using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {
    static List<CharacterController2D> players;

	// Use this for initialization
	static void Start () {
        players = new List<CharacterController2D>();
	}
    

    static void RegisterWithInputManager(CharacterController2D controller)
    {
        players.Add(controller);
    }

    public static void ResetInputManager()
    {
        Start();
    }

    static int GetPlayerNumber(CharacterController2D controller)
    {
        return players.IndexOf(controller);
    }

    public static float GetAxis(string axisName, CharacterController2D controller)
    {
        //Debug.Log("GetAxis " + axisName + controller.playerNumber);
        return Input.GetAxis(axisName + controller.playerNumber);
    }

    public static float GetAxisRaw(string axisName, CharacterController2D controller)
    {
        //Debug.Log("GetAxisRaw " + axisName + controller.playerNumber);
        return Input.GetAxisRaw(axisName + controller.playerNumber);
    }

    public static bool GetButton(string axisName, CharacterController2D controller)
    {
        Debug.Log("GetButton " + axisName + controller.playerNumber);
        return Input.GetButton(axisName + controller.playerNumber);
    }

    public static bool GetButtonDown(string axisName, CharacterController2D controller)
    {
        Debug.Log("GetButtonDown " + axisName + controller.playerNumber);
        return Input.GetButtonDown(axisName + controller.playerNumber);
    }
}
