using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public float 	startDelay;
	public float 	sessionLength;
	public int		poseState; // 0 = unfinished, 1 = right, 2 = wrong

	public enum GameState {
		HomeScreen,
		Starting,
		Active,
		Ending
	}

	private GameState currentState;
	public GameState CurrentState => currentState;

	public static GameManagerScript S;
	private WaitForSeconds startWait;
	private bool isTransitioning = false;

	private void Awake(){
		S = this;
		DontDestroyOnLoad (this);
		currentState = GameState.HomeScreen;
	}

	// Use this for initialization
	private void Start () {
		startWait = new WaitForSeconds (startDelay);
		StartCoroutine (GameLoop ());
	}
	
	private IEnumerator GameLoop(){
		while (true) {
			switch (currentState) {
				case GameState.HomeScreen:
					yield return StartCoroutine (HomeScreen ());
					break;
				case GameState.Starting:
					yield return StartCoroutine (SessionStarting ());
					break;
				case GameState.Active:
					yield return StartCoroutine (SessionActive ());
					break;
				case GameState.Ending:
					yield return StartCoroutine (SessionEnding ());
					// Optional: return to main screen after ending
					currentState = GameState.HomeScreen;
					break;
			}
			yield return null;
		}
	}

	private IEnumerator HomeScreen(){
		// Wait until player starts the game
		while (currentState == GameState.HomeScreen) {
			yield return null;
		}
	}

	private IEnumerator SessionStarting(){
		if (isTransitioning) yield break;
		isTransitioning = true;

		ComboScript.S.ChangeCombo();

		AudioScript.S.Welcome();
		yield return startWait;

		currentState = GameState.Active;
		isTransitioning = false;
	}
		
	private IEnumerator SessionActive(){
		yield return new WaitForSeconds (1);
		StartCoroutine (TimerScript.S.SessionTimer ());
		
		while (!TimerScript.S.timesUp && currentState == GameState.Active) {
			GetState ();
			yield return null;
		}

		if (TimerScript.S.timesUp) {
			currentState = GameState.Ending;
		}
	}

	private IEnumerator SessionEnding(){
		if (isTransitioning) yield break;
		isTransitioning = true;

		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("GameOver");
		isTransitioning = false;
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
		if (currentState == GameState.HomeScreen && !isTransitioning) {
			Debug.Log ("Starting game");
			currentState = GameState.Starting;
		}
	}
}

