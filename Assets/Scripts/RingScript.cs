using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour {

	public float				minSize;
	public float				growSpeed;
	public float				opacitySpeed;
	public float				opacityMin = 0.5f;

	private bool				pressed; 
	private float				size;
	private float 				opacity;
	private SpriteRenderer		spriteRenderer;

	public static RingScript 	S;

    
	// Use this for initialization
	void Start () {
		S = this;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		Reset ();
	}

	public void Down(){
		if (spriteRenderer == null) {
			spriteRenderer = GetComponent<SpriteRenderer>();
			if (spriteRenderer == null) {
				return;
			}
		}
		pressed = true;
		StartCoroutine (Hold ());
	}

	private IEnumerator Hold(){
		if (spriteRenderer == null) {
			yield break;
		}
		size = minSize;
		opacity = 1f;  // Initialize opacity
		while (pressed) {
			size += (Time.deltaTime * growSpeed);
			opacity = Mathf.Max(opacityMin, opacity - (Time.deltaTime * opacitySpeed));
			this.transform.localScale = new Vector3 (size, size, 1);
			spriteRenderer.color = new Color (1f, 1f, 1f, opacity);
			yield return null;
		}
		Debug.Log("Hold coroutine ended");
		yield break;
	}			

	public void Wrong(){
		StartCoroutine (WrongFeedback ());
	}

	private IEnumerator WrongFeedback(){
		Color originalColor = spriteRenderer.color;
		spriteRenderer.color = new Color (1f, 0.3f, 0.3f, originalColor.a);
		yield return new WaitForSeconds (0.2f);
		spriteRenderer.color = originalColor;
	}

	public void Up(){
		pressed = false;
		if(size >= minSize) StartCoroutine (Release ());
	}

	private IEnumerator Release(){
		while (size > minSize) {
			size -= (Time.deltaTime * 2f);
			opacity = opacityMin;
			this.transform.localScale = new Vector3 (size, size, 1);
			spriteRenderer.color = new Color (1f, 1f, 1f, opacity);
			yield return null;
		}
		this.transform.localScale = new Vector3 (minSize, minSize, 1);
		yield break;
	}

	public void Reset(){
		pressed = false;
		size = minSize;
		opacity = 1f;  // Initialize opacity
		this.transform.localScale = new Vector3(minSize, minSize, 1);
		if(spriteRenderer != null) {
			spriteRenderer.color = new Color(1f, 1f, 1f, 1f);  // Reset color with full opacity
		}
	}
}
