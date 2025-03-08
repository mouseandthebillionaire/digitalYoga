using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour {

	public Image	homeScreenOverlay;

	public static UIscript	S;

	// Use this for initialization
	void Start () {
		S = this;
		HomeScreen ();
	}
		
	public void HomeScreen(){
		homeScreenOverlay.gameObject.SetActive (true);
		homeScreenOverlay.color = new Color (1, 1, 1, 1);
	}


	public void FadeIn(){
		//homeScreenOverlay.gameObject.SetActive (false);
		StartCoroutine (FadeOutHomeScreen(0));
	}
		
	private IEnumerator FadeOutHomeScreen(int fadeDest){
		float a = homeScreenOverlay.color.a;
		yield return new WaitForSeconds (1);
		while (a > fadeDest) {
			a -= 0.25f * Time.deltaTime;
			homeScreenOverlay.color = new Color (1, 1, 1, a);
			yield return null;
		}
		homeScreenOverlay.gameObject.SetActive (false);
		yield break;
	}
		
}
