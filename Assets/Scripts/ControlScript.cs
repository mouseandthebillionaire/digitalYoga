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
	public float					topBuffer = 60f;     // Buffer from top of screen
	public float					bottomBuffer = 60f;   // Buffer from bottom of screen
	public float					leftBuffer = 40f;     // Buffer from left of screen
	public float					rightBuffer = 40f;    // Buffer from right of screen
	private float					sWidth, sHeight, buttonWidth, buttonHeight;
	private float					usableWidth, usableHeight;

	// Button
	public GameObject				b;
	private Transform				buttonCanvas;

	public List<string>				buttonsPressed = new List<string>();
	public static int				numRight;
	public static int				numPressed;


	private GameObject[,]			Grid;
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
		// Calculate usable screen dimensions after applying buffers
		sHeight = Screen.height;
		sWidth = Screen.width;
		usableWidth = sWidth - (leftBuffer + rightBuffer);
		usableHeight = sHeight - (topBuffer + bottomBuffer);
		
		buttonWidth = usableWidth / columns;
		buttonHeight = usableHeight / rows;

		buttonCanvas = GameObject.Find("buttonGrid").transform;

		Grid = new GameObject[columns, rows];

		for(int i=0; i < columns; i++){
			for(int j=0; j < rows; j++){

				GameObject go = GameObject.Instantiate(b) as GameObject;
				
				// Calculate position with buffers
				float xPos = leftBuffer + (buttonWidth * i);
				float yPos = bottomBuffer + (buttonHeight * j);
				Vector3 pos = new Vector3(xPos, yPos, 0);

				go.transform.SetParent(buttonCanvas);

				go.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, buttonHeight);
				go.transform.position = pos;

				int num = i * rows + j;
				go.name = num.ToString();
				Grid[i,j] = go;
			}
		}
	}

	public void Add(string buttonNum){
		numPressed += 1;
		combo = ComboScript.S.currCombo;
		buttonsPressed.Add(buttonNum);
		if (System.Array.IndexOf(combo, buttonNum) != -1) {
			numRight++;
			mallets.PlayOneShot(mNotes[numRight]);
		} else {
			nope.Play();
		}
		if (numPressed == combo.Length) {
			if (numRight == combo.Length) {
				// Increase the score
				ScoreScript.S.Score(1);
			} else {
				// Decrease the score!
				ScoreScript.S.Score(-1); 
			}
		}
	}

	public void Remove(string buttonNum){
		if(System.Array.IndexOf(combo, buttonNum) != -1){
			if(numRight > 0) numRight--;
		}
		if (numPressed > 0) {
			numPressed -= 1;
			buttonsPressed.Remove(buttonNum);
		}
	}

	public void Reset(){
		numRight = 0;
		numPressed = 0;
		buttonsPressed.Clear();
		
		// Broadcast Reset message to all circle scripts
		buttonCanvas.BroadcastMessage("Reset", SendMessageOptions.DontRequireReceiver);
	}



}
