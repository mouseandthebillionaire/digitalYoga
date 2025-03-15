using UnityEngine;
using System.Collections;

public class ComboScript : MonoBehaviour {

	public TextAsset				comboList;
	public string[]					combos;
	public string[]					currCombo;
	public static int				currComboLength;
	public static int				currPose;
	public static int				poseNum;
	public static int 				c;

	private Transform               buttonGrid;

	public static ComboScript		S;

	
	// Use this for initialization
	void Awake () {
		S = this;
		buttonGrid = GameObject.Find("buttonGrid").transform;
	}

	void Start(){
		combos = comboList.text.Split('\n');
		poseNum = 0;
		currCombo = GetCombo(0);
		currComboLength = 1;
		UpdateActiveCircles();
	}
	
	// Update is called once per frame
	void Update () {
		currComboLength = currCombo.Length;
	}

	public void ChangeCombo(){
		int _c = 0;
		if (poseNum <= 5){
			_c = poseNum;
		} else {
			_c = Random.Range(6, 18);
		}
		c = _c;
		// Get New Combo
		currCombo = GetCombo(c);
		AudioScript.S.IntroducePose(c);
		poseNum += 1;
		UpdateActiveCircles();
	}

	private void UpdateActiveCircles() {
		if (buttonGrid == null) {
			buttonGrid = GameObject.Find("buttonGrid").transform;
			if (buttonGrid == null) return;
		}

		// First set all circles to inactive opacity
		foreach (Transform child in buttonGrid) {
			circleScript circle = child.GetComponentInChildren<circleScript>();
			if (circle != null) {
				circle.SetInactive();
			}
		}

		// Then set the active ones to full opacity
		foreach (string buttonNum in currCombo) {
			Transform buttonTransform = buttonGrid.Find(buttonNum);
			if (buttonTransform != null) {
				circleScript circle = buttonTransform.GetComponentInChildren<circleScript>();
				if (circle != null) {
					circle.SetActive();
					Debug.Log("Active");
				}
			}
		}
	}

	public string[] GetCombo (int p){
		string thisCombo = combos[p];
		string[] letters = thisCombo.Split(',');
		string[] numbers = new string[letters.Length];
		for(int i=0; i < letters.Length; i++){
			// Convert letter to corresponding button number
			char letter = letters[i][0]; // Take first character since we might have whitespace
			int buttonNum = GetButtonNumberFromLetter(letter);
			numbers[i] = buttonNum.ToString();
		}
		return numbers;
	}

	// Helper method to convert from old letter system to new number system
	private int GetButtonNumberFromLetter(char letter) {
		// Using the old ButtonCode array order to maintain compatibility with existing combo files
		string[] oldButtonCode = { "b", "v", "c", "x", "z", "g", "f", "d", "s", "a", "t", "r", "e", "w", "q" };
		string letterStr = letter.ToString().ToLower();
		int index = System.Array.IndexOf(oldButtonCode, letterStr);
		return index >= 0 ? index : 0; // Return 0 if letter not found (shouldn't happen with valid combo file)
	}
}
