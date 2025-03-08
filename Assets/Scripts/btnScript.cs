using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class btnScript : MonoBehaviour {

	public string				name;
	public bool					on;
	public GameObject			b;
	public Color[]				colors;
	public Color				currColor;
	private int 				currComboNum;
	public string               buttonLetter;

	static public btnScript		S;

	// Use this for initialization
	void Start () {
		b = this.gameObject;
		name = b.name;
		on = false;
		currColor = colors[0];
	    buttonLetter = ControlScript.S.ButtonCode[int.Parse(name)];
	}

	public void Down(){
		// Get the current combo
		currComboNum = ComboScript.poseNum;

		ControlScript.S.Add(name);
		if(System.Array.IndexOf(ControlScript.S.combo, buttonLetter) != -1){
			circleGridScript.S.Press (name);
			currColor = colors[1];
		} else {
			circleGridScript.S.Wrong (name);
			currColor = colors[2];
		}
	}

	public void Up(){
		// Are we still in the same combo
		int thisComboNum = ComboScript.poseNum;
		if (thisComboNum == currComboNum) {
			ControlScript.S.Remove (name);
			circleGridScript.S.Unpress (name);
			currColor = colors [0];
		}
		// otherwise don't do anything
	}

	public void Reset(){
		currColor = colors[0];
	}
}
