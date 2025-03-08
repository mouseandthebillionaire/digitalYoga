using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleScript : MonoBehaviour {

	public float				minSize;

	private bool				pressed; 
	private float				size;
	private float 				opacity;
	private float 				c = 1f;
	private Material			m;

	public static circleScript 	S;

    
	// Use this for initialization
	void Start () {
		S = this;
		pressed = false;
		opacity = 1f;
		m = this.GetComponent<Renderer> ().material;
		Reset ();
	}

	public void Down(){
		pressed = true;
		c = 1;
		StartCoroutine (Hold ());
	}

	private IEnumerator Hold(){
		size = minSize;
		while (pressed) {
			size += (Time.deltaTime * .075f);
			opacity -= (Time.deltaTime * .075f);
			this.transform.localScale = new Vector3 (size, size, 1);
			m.color = new Color (c, c, c, opacity);
			yield return null;
		}
		yield break;
	}			

	public void Wrong(){
//		c = 0;
//		m.color = new Color (c, c, c, 1);
	}

	public void Up(){
		pressed = false;
		// c = 1f;
		if(size >= minSize) StartCoroutine (Release ());
	}

	private IEnumerator Release(){
		while (size > minSize) {
			size -= (Time.deltaTime * 2f);
			opacity += (Time.deltaTime * 2f);
			this.transform.localScale = new Vector3 (size, size, 1);
			m.color = new Color (c, c, c, opacity);
			yield return null;
		}
		this.transform.localScale = new Vector3 (minSize, minSize, 1);
		yield break;
	}

	public void Reset(){
		pressed = false;
		c = 1f;
		opacity = 0.3f;
		this.transform.localScale = new Vector3 (minSize, minSize, 1);
		StartCoroutine (Assess ());
	}

	private IEnumerator Assess(){
		yield return new WaitForSeconds (0.5f);
		// Assess if you exist in the new combo
		string bta = ControlScript.S.ButtonCode [int.Parse (this.name)];
		if (System.Array.IndexOf (ComboScript.S.currCombo, bta) != -1) {
			opacity = 1f;
		} else {
			opacity = 0.3f;
		} 
		m.color = new Color (1, 1, 1, opacity);
		yield break;
	}
}
