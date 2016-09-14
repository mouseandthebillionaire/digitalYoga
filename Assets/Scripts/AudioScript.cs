using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class AudioScript : MonoBehaviour {

	//Audio
	public AudioSource				yup;
	public AudioClip[]				yMelodies;

	public AudioMixerSnapshot[] 	snapshots;
	public float					t_time;

	public static					AudioScript S;

	// Use this for initialization
	void Awake () {
		S = this;
	}

	void Update() {
		int state = ControlScript.state;
		// Test
		if (Input.GetKey("up")) {
			state = 0;
			Debug.Log("U");
		}
		if (Input.GetKey("down")) {
			state = 1;
			Debug.Log("D");
		}

		if(state == 0) {			// Active
			t_time = 2f;
		} else if(state == 2) {		// Nope
			t_time = 5f;
		} else {					// Yup
			int yL = Random.Range (0, yMelodies.Length);
			yup.PlayOneShot(yMelodies[yL], 0.25f);
			t_time = 0.1f;
		}
			
		snapshots[state].TransitionTo(t_time);

	}
}
