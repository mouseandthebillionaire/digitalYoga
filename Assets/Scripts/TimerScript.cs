using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimerScript : MonoBehaviour {
	
	public float 				t = 0;
	public float 				poseLength = 15f;
	private float 				poseTimer = 0.0f;
	private float 				elapsedTime;
	public bool 				timesUp = true;
	public Image				p;

	// Countdown Timer
	private float 				timeLeft;
	private float 				minutes;
	private float 				seconds;
	public Text 				timerText;


	public static TimerScript	S;

	// Use this for initialization
	void Start () {
		S = this;
		t = 0;
		p.fillAmount = 0;
		GetTime ();
	}
	
	// Update is called once per frame
	public IEnumerator SessionTimer () {
		timesUp = false;
		timeLeft = GameManagerScript.S.sessionLength;
		StartCoroutine (PoseTimer ());
		elapsedTime = Time.deltaTime;
		while (timeLeft > 0) {
			// do the countdown
			GetTime();
			yield return null;
		}
		timesUp = true;
		yield break;
	}

	public void GetTime(){
		timeLeft -= Time.deltaTime;

		minutes = Mathf.Floor (timeLeft / 60);
		seconds = timeLeft % 60;
		if (seconds > 59)
			seconds = 59;
		if (minutes < 0) {
			minutes = 0;
			seconds = 0;
		}
		timerText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
	}

		

	public IEnumerator PoseTimer(){
		// bar filling screen showing length to hold pose
		poseTimer = Time.time;
		while(!timesUp){
			float poseTime = Time.time - poseTimer;
			t = poseTime / poseLength;
			p.fillAmount = t;

			if (t > 1f) {
				Next ();
			}

			yield return null;
		}
	}
	
	public void Next() {
		//PoseScript.S.ChangePose();
		poseTimer = Time.time;
		ComboScript.S.ChangeCombo();
		ControlScript.S.Reset ();

	}
}