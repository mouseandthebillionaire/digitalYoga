using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {

	public static int			state; // 0 = none, 1 = right, 2 = wrong
	public static				ControlScript S;

	// Update is called once per frame
	void Update () {
		S = this;
		GetState();
		KeyState();
		//Debug.Log("Stater: " + state + " / " + buttonScript.numRight + " / " + buttonScript.numPressed);
	}

	void GetState(){
		if(buttonScript.numPressed < 5) {
			state = 0;
		} else {
			if (buttonScript.numRight == 5) state = 1;
			else state = 2;
		}
	}
	void KeyState(){
		if(Input.GetKey("q")) state = 0;
		if(Input.GetKey("w")) state = 1;
		if(Input.GetKey("e")) state = 2;
	}
		
}
