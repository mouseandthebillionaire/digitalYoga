using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	public Text 		endText;
	public int			finalScore;
	public AudioSource  om;
	public string		shareText, scoreText;

	private const string TWITTER_ADDRESS = "http://twiter.com/intent/tweet";
	private const string TWEET_LANGUAGE = "en";

	// Use this for initialization
	void Start () {
		finalScore = ScoreScript.S.score;
		scoreText = finalScore.ToString ();
		shareText = "I just completed my Digital Yoga Practice";
		endText.text = "Thank you for practicing with us today. \n\n Namaste.";
		PlayerPrefs.SetInt("Score", finalScore);
	}

	public void Reset(){
		SceneManager.LoadScene("Main");
	}

	public void How(){
		SceneManager.LoadScene("Instructions");
	}

	public void ShareToTwitter (){
		Application.OpenURL(TWITTER_ADDRESS +
		                    "?text=" + WWW.EscapeURL(shareText) +
		                    "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}
}
