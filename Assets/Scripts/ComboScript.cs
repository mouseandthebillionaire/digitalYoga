using UnityEngine;
using System.Collections;

public class ComboScript : MonoBehaviour {

	public TextAsset				comboList;
	public string[]					combos;
	public string[]					currCombo;
	public static int				currPose;
	public static int 				c;

	public static ComboScript		S;

	
	// Use this for initialization
	void Start () {
		S = this;
		combos = comboList.text.Split ('\n');
		currPose = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(PoseScript.p != currPose){
			currPose = PoseScript.p;
			currCombo = GetCombo (currPose);
		}
		//Debug.Log(currCombo[0] + currCombo[1] + currCombo[2] + currCombo[3] + currCombo[4]);
	}

	public void ChangeCombo(){
		int _c = Random.Range(0, combos.Length);
		while (c == _c){
			_c = Random.Range(0, combos.Length);
		}
		c = _c;
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
