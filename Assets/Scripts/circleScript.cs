using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class circleScript : MonoBehaviour {

	public float				minSize;
	public float				growSpeed;
	public float				opacitySpeed;
	public float				opacityMin = 0.5f;

	private bool				pressed; 
	private bool             isActive;
	private float				size;
	private float 				opacity;
	private Image				img;

	public static circleScript 	S;

    
	// Use this for initialization
	void Start () {
		S = this;
		img = this.GetComponent<Image> ();
		Reset ();
	}

	public void Down(){
		pressed = true;
		StartCoroutine (Hold ());
	}

	private IEnumerator Hold(){
		size = minSize;
		while (pressed) {
			size += (Time.deltaTime * growSpeed);
			if (isActive) {
				opacity = Mathf.Max(opacityMin, opacity - (Time.deltaTime * opacitySpeed));
			}
			this.transform.localScale = new Vector3 (size, size, 1);
			img.color = new Color (1f, 1f, 1f, opacity);
			yield return null;
		}
		yield break;
	}			

	public void Wrong(){
		StartCoroutine (WrongFeedback ());
	}

	private IEnumerator WrongFeedback(){
		Color originalColor = img.color;
		img.color = new Color (1f, 0.3f, 0.3f, originalColor.a);
		yield return new WaitForSeconds (0.2f);
		img.color = originalColor;
	}

	public void Up(){
		pressed = false;
		if(size >= minSize) StartCoroutine (Release ());
	}

	private IEnumerator Release(){
		while (size > minSize) {
			size -= (Time.deltaTime * 2f);
			opacity = isActive ? 1f : opacityMin;
			this.transform.localScale = new Vector3 (size, size, 1);
			img.color = new Color (1f, 1f, 1f, opacity);
			yield return null;
		}
		this.transform.localScale = new Vector3 (minSize, minSize, 1);
		UpdateOpacity();
		yield break;
	}

	public void Reset(){
		pressed = false;
		size = minSize;
		this.transform.localScale = new Vector3(minSize, minSize, 1);
	}

	public void SetActive() {
		isActive = true;
		UpdateOpacity();
	}

	public void SetInactive() {
		isActive = false;
		UpdateOpacity();
	}

	private void UpdateOpacity() {
		opacity = isActive ? 1f : opacityMin;
		img.color = new Color (1f, 1f, 1f, opacity);
	}
}
