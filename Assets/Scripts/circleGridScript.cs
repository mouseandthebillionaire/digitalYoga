using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleGridScript : MonoBehaviour {

	public int						rows, columns;
	private float					sWidth, sHeight, circleWidth, circleHeight;
	// Circle
	public GameObject				c;
	public Transform				circleTransform;

	public GameObject[]				circles;

	public static circleGridScript S;

	// Use this for initialization
	void Start () {
		S = this;
		sHeight = 7f;
		sWidth = Screen.width;
		circleWidth = sWidth / columns;
		circleHeight = sHeight / rows;


		circles = new GameObject[columns * rows];

		circleTransform = GameObject.Find("CircleGrid").transform;

		for(int i=0; i < columns; i++){
			for(int j=0; j < rows; j++){

				GameObject go = GameObject.Instantiate(c) as GameObject;
				Vector3 pos = new Vector3 (i * 1.75f - 1.75f, circleHeight * j - 3f, 0f);
				//Vector3 pos = new Vector3(circleWidth * i, circleHeight * j, 0);


				go.transform.parent = circleTransform;

				go.transform.position = pos;

				int num = i * rows + j;
				go.name = num.ToString();
				circles[num] = go;
			}
		}
	}

	public void Press(string s){
		int i = int.Parse(s);
		circles[i].GetComponent<circleScript>().Down();
	}

	public void Wrong(string s){
		int i = int.Parse(s);
		circles[i].GetComponent<circleScript>().Wrong();
	}

	public void Unpress(string s){
		int i = int.Parse(s);
		circles[i].GetComponent<circleScript>().Up();
	}

	public void Reset(){
		for (int i = 0; i < circles.Length; i++) {
			circles [i].GetComponent<circleScript> ().Reset ();
		}
	}
		
}
