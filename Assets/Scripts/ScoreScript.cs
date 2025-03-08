using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ScoreScript : MonoBehaviour {

	public int					score;

	public static ScoreScript	S;

	// Use this for initialization
	void Start () {
		score = 0;
		S = this;
	}

	public void Score(int dir){
		// 1 = correct, -1 = incorrect
		if (score >= 0  && score < 115) {
			score += 10 * dir;
		}
	}

}
