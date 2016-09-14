using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Color[]		stateColors;
		
	// Update is called once per frame
	void Update () {
		int s = ControlScript.state;
 		this.GetComponent<SpriteRenderer>().color = stateColors[s];
	}
}
