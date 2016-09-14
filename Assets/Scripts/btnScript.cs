using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class btnScript : MonoBehaviour {

	public string				name;
	public bool					on;
	public GameObject			b;
	public Color[]				colors;
	public Color				currColor;

	static public btnScript		S;

	// Use this for initialization
	void Start () {
		b = this.gameObject;
		name = b.name;
		on = false;
		currColor = colors[0];
	}

	void Update() {
		b.GetComponent<Image>().color = currColor;
	}

	public void Down(){
		buttonScript.S.Add(name);
		string bta = buttonScript.S.ButtonCode[int.Parse (name)];
		if(System.Array.IndexOf(buttonScript.S.combo, bta) != -1){
			currColor = colors[1];
		} else {
			currColor = colors[2];
		}
	}

	public void Up(){
		buttonScript.S.Remove(name);
		currColor = colors[0];
	}

	public void Reset(){
		currColor = colors[0];
	}
}
