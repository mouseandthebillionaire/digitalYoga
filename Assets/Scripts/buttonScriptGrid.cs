using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buttonScriptGrid : MonoBehaviour {
	
	public int						buttonSize;
	public GameObject				b;
	public List<string>				buttonsPressed = new List<string>();
	public static int				numRight;
	public static int				numPressed;

	public int 						numButtons = 15;
	public GameObject[]				btns;


	public string[]					ButtonCode = { "b","v","c","x","z","g","f","d","s","a","t","r","e","w","q" };
	public string[]					combo;

	public static 					buttonScriptGrid S;

	void Awake(){
		S = this;
	}

	void Start () {
		btns = new GameObject[numButtons];
		for(int i=0; i < numButtons; i++){
			GameObject go = GameObject.Instantiate(b) as GameObject;
			go.transform.parent = GameObject.Find("buttonGrid").transform;
			int num = i;
			go.name = num.ToString();
			btns[i] = go;
		}
	}

	void Update(){
		combo = ComboScript.S.currCombo;
		numPressed = buttonsPressed.Count;
		Debug.Log(buttonsPressed[0]);
	}

	public void Add(string b){
		string bta = ControlScript.S.ButtonCode[int.Parse (b)];
		if(System.Array.IndexOf(ControlScript.S.combo, bta) != -1){
			numRight++;
		}
		buttonsPressed.Add (bta);
	}

	public void Remove(string b){
		string btr = ButtonCode[int.Parse(b)];
		if(System.Array.IndexOf(combo, btr) != -1){
			if(numRight > 0) numRight--;
		}
		if (numPressed > 0) buttonsPressed.Remove(btr);
	}

	public void Reset(){
		numRight = 0;
		numPressed = 0;
		buttonsPressed.Clear();
	}


}
