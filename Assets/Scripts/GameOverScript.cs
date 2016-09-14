using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	public Text 		endText;
	public int			finalScore;
	public AudioSource  om;
	public string		shareText;

	private const string TWITTER_ADDRESS = "http://twiter.com/intent/tweet";
	private const string TWEET_LANGUAGE = "en";

	// Use this for initialization
	void Start () {
		//finalScore = ScoreScript.score;
	}
	
	// Update is called once per frame
	void Update () {
		endText.text = "You have successfully filled your Spririt. \n\n Namaste.";
		//om.Play();
		shareText = "I just completed my Digital Yoga Practice";


	}

	public void Again(){
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
