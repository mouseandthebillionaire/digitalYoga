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

	public static ComboScript		S;

	
	// Use this for initialization
	void Awake () {
		S = this;
		combos = comboList.text.Split ('\n');
		poseNum = 0;
		//currPose = 0;
		currCombo = GetCombo(0);
		currComboLength = 1;
	}
	
	// Update is called once per frame
	void Update () {
//		if(c != currPose){
//			currPose = c;
//			currCombo = GetCombo (currPose);
//		}
		currComboLength = currCombo.Length;
	}

	public void ChangeCombo(){
		int _c = 0;
		if (poseNum <= 5){
			_c = poseNum;
		} else {
			//_c = Random.Range(6, combos.Length);
			_c = Random.Range(6, 18);
		}
		c = _c;
		// Get New Combo
		currCombo = GetCombo(c);
		// Test to run through full game on computer with only one keypress
		// currCombo = GetCombo(0);
		AudioScript.S.IntroducePose(c);
		poseNum += 1;
	}

	public string[] GetCombo (int p){
		string thisCombo = combos[p];
		string[] _letters = new string[thisCombo.Length];
		for(int i=0; i<thisCombo.Length; i++){
			_letters[i] = thisCombo[i].ToString();
		}
		return _letters;

	}
}
