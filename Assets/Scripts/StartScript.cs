using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	public Text 		startText;
	public AudioSource  om;
	public Image		logo;
	public Image 		overlay;

	private bool		launching;
	private float		a;

	public void Start(){
		a = overlay.color.a;
	}

	private IEnumerator Launch(){
		while (a < 1f) {
			a += 0.01f;
			overlay.color = new Color (overlay.color.r, overlay.color.g, overlay.color.b, a);
		}
		SceneManager.LoadScene("Main");
		yield break;
	}

	public void Launching(){
		StartCoroutine (Launch ());
	}

	public void How(){
		SceneManager.LoadScene("Instructions");
	}
}
