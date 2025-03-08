using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControlScript : MonoBehaviour {

	/* 
	 * 
	 * Make this the main Control script of the game
	 * 
	*/

	public int						rows, columns;
	private float					sWidth, sHeight, buttonWidth, buttonHeight;

	// Button
	public GameObject				b;
	private Transform				buttonCanvas;

	public List<string>				buttonsPressed = new List<string>();
	public static int				numRight;
	public static int				numPressed;


	private GameObject[,]			Grid;


	public string[]                 ButtonCode = { "b", "v", "c", "x", "z", "g", "f", "d", "s", "a", "t", "r", "e", "w", "q" };
	public string[]					combo;

	//Audio
	public AudioSource				mallets, nope;
	public AudioClip[]				mNotes;

	public static 					ControlScript S;

	void Awake(){
		S = this;
		numPressed = 0;
	}

	void Start () {
		sHeight = Screen.height - 60f;
		sWidth = Screen.width;
		buttonWidth = sWidth / columns;
		buttonHeight = sHeight / rows;

		buttonCanvas = GameObject.Find("buttonGrid").transform;

		Grid = new GameObject[columns, rows];

		for(int i=0; i < columns; i++){
			for(int j=0; j < rows; j++){

				GameObject go = GameObject.Instantiate(b) as GameObject;
				Vector3 pos = new Vector3(buttonWidth * i, buttonHeight * j, 0);


				go.transform.SetParent (buttonCanvas);

				go.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, buttonHeight);
				go.transform.position = pos;

				int num = i * rows + j;
				go.name = num.ToString();
				Grid[i,j] = go;
			}
		}
	}

	public void Add(string b){
		numPressed += 1;
		combo = ComboScript.S.currCombo;
		string bta = ButtonCode[int.Parse (b)];
		buttonsPressed.Add (bta);
		if (System.Array.IndexOf (combo, bta) != -1) {
			numRight++;
			mallets.PlayOneShot (mNotes [numRight]);
		} else {
			nope.Play ();
		}
		if (numPressed == combo.Length) {
			if (numRight == combo.Length) {
				// Increase the score
				ScoreScript.S.Score(1);
			} else {
				// Deacrease the score!
				ScoreScript.S.Score(-1); 
			}
		}

	}


	public void Remove(string b){
		string btr = ButtonCode[int.Parse(b)];
		if(System.Array.IndexOf(combo, btr) != -1){
			if(numRight > 0) numRight--;
		}
		if (numPressed > 0) {
			numPressed -= 1;
			buttonsPressed.Remove(btr);
		}
	}

	public void Reset(){
		numRight = 0;
		numPressed = 0;
		buttonsPressed.Clear();
		circleGridScript.S.Reset ();
	}


}
