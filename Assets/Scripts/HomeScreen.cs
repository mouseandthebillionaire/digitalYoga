using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HomeScreen : MonoBehaviour
{

	private Image[]	homeScreenImages;
    

	public static HomeScreen	S;

	private void Awake(){
        S = this;
    }
    
    // Use this for initialization
	void Start () {
		homeScreenImages = GetComponentsInChildren<Image>();
        DisplayHomeScreen ();
	}

    protected virtual void OnTap(){
        FadeOut();
    }

    void OnMouseDown(){
        Debug.Log ("Mouse down");
        FadeOut();
    }
		
	public void DisplayHomeScreen(){
		this.gameObject.SetActive (true);
		for(int i = 0; i < homeScreenImages.Length; i++){
			homeScreenImages[i].color = new Color (1, 1, 1, 1);
		}
	}


	public void FadeOut(){
		//homeScreenOverlay.gameObject.SetActive (false);
		StartCoroutine (FadeOutHomeScreen(0));
	}
		
	private IEnumerator FadeOutHomeScreen(int fadeDest){
		float a = 1.0f;
		yield return new WaitForSeconds (1);
		while (a > fadeDest) {
			a -= 0.25f * Time.deltaTime;
            for(int i = 0; i < homeScreenImages.Length; i++){
                homeScreenImages[i].color = new Color (1, 1, 1, a);
            }
			yield return null;
		}
		this.gameObject.SetActive (false);
        GameManagerScript.S.homeScreen = false;
        GameManagerScript.S.StartGame();
		yield break;
	}
}
