using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Color[]			stateColors;
	public SpriteRenderer	sr;
	public Camera			c;
	public float			t;

	public float 			hue;	
	public float			value;

	void Awake(){
		hue = 0.005f;
		sr = this.GetComponent<SpriteRenderer> ();
	}


	// Update is called once per frame
	void Update () {
		int s = GameManagerScript.S.poseState;

		switch(s){
		case 0:
			// do nothing
			ResetGlitch ();
			break;
		case 1:
			// You've got the right pose
			ResetGlitch ();
			float newHue = ScoreScript.S.score / 100f;
			hue = Mathf.Lerp (hue, newHue, 0.025f * Time.deltaTime);
            break;

		case 2:
			// You've got the wrong pose
			value -= .001f;
			if (value < .6f) {
				t += .0005f;
			}
			break;
		}

		sr.color = Color.HSVToRGB (hue, 0.32f, value);
		c.GetComponent<Kino.DigitalGlitch> ().intensity = t;

	}

	public void ResetGlitch(){
		t = 0;
		value = .95f;
	}
}
