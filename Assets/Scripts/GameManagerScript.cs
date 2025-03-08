using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour {

	public float 	startDelay;
	public float 	sessionLength;
	public int		poseState; // 0 = unfinished, 1 = right, 2 = wrong


	// Terrible way of doing this? Fix later.
	public bool homeScreen, sessionActive;

	public static GameManagerScript S;

	private WaitForSeconds startWait;

	private void Awake(){
		S = this;
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	private void Start () {
		homeScreen = true;
		startWait = new WaitForSeconds (startDelay);
		StartCoroutine (GameLoop ());
		
	}
	
	private IEnumerator GameLoop(){
		yield return StartCoroutine (MainScreen ());
		yield return StartCoroutine (SessionStarting ());
		yield return StartCoroutine (SessionActive ());
		yield return StartCoroutine (SessionEnding ());
	}

	private IEnumerator MainScreen(){
		sessionActive = false;
		while (homeScreen) {
			yield return null;
		}
	}

	private IEnumerator SessionStarting(){
		// Use this to set up the new Session
		UIscript.S.FadeIn();
		AudioScript.S.Welcome();
		yield return startWait;
	}
		
	private IEnumerator SessionActive(){
		yield return new WaitForSeconds (1);
		StartCoroutine (TimerScript.S.SessionTimer ());
		while (!TimerScript.S.timesUp) {
			GetState ();
			yield return null;
		}
	}

	private IEnumerator SessionEnding(){
		sessionActive = false;
		//BroadcastMessage ("GameOver");
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("GameOver");
		yield return null;
	}

	public void GetState(){
		int comboLength = ComboScript.S.currCombo.Length;
		if(ControlScript.numPressed < comboLength) {
			// Not finished posing
			poseState = 0;
		} else {
			if (ControlScript.numRight == comboLength) {
				// This is the correct pose
				poseState = 1;
				//ScoreScript.S.Score (1);
			} else {
				// This pose is incorrect
				poseState = 2;
				//ScoreScript.S.Score (-1);
			}
		}
	}

	public void StartGame(){
		sessionActive = true;
		homeScreen = false;
	}


}

