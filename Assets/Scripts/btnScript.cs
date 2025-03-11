using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class btnScript : MonoBehaviour {

	public string				name;
	public bool					on;
	public GameObject			b;
	//public Color[]			colors;
	//public Color				currColor;
	private int 				currComboNum;

	public GameObject			ring;

	static public btnScript		S;

	// Use this for initialization
	void Start () {
		b = this.gameObject;
		name = b.name;
		on = false;
		//currColor = colors[0];
	}

	public void Down(){
		// Get the current combo
		currComboNum = ComboScript.poseNum;

		ControlScript.S.Add(name);
		if(System.Array.IndexOf(ControlScript.S.combo, name) != -1){
			ring.GetComponent<circleScript>().Down();
			//currColor = colors[1];
		} else {
			//currColor = colors[2];
		}
	}

	public void Up(){
		// Are we still in the same combo
		int thisComboNum = ComboScript.poseNum;
		if (thisComboNum == currComboNum) {
			ControlScript.S.Remove(name);
			ring.GetComponent<circleScript>().Up();
			//currColor = colors [0];
		}
		// otherwise don't do anything
	}

	public void Reset(){
		//currColor = colors[0];
		ring.GetComponent<circleScript>().Up();
	}
}
