using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	public Text 		startText;
	public AudioSource  om;

	// Update is called once per frame
	void Update () {
		startText.text = "Welcome to Digital Yoga. \n\n Shall we begin our journey.";
		//om.Play();
	}

	public void Begin(){
		SceneManager.LoadScene("Main");
	}

	public void How(){
		SceneManager.LoadScene("Instructions");
	}
}
