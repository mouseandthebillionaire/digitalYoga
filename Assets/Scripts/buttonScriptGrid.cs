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

	public void Add(string buttonNum){
		if(System.Array.IndexOf(ComboScript.S.currCombo, buttonNum) != -1){
			numRight++;
		}
		buttonsPressed.Add(buttonNum);
	}

	public void Remove(string buttonNum){
		if(System.Array.IndexOf(combo, buttonNum) != -1){
			if(numRight > 0) numRight--;
		}
		if (numPressed > 0) buttonsPressed.Remove(buttonNum);
	}

	public void Reset(){
		numRight = 0;
		numPressed = 0;
		buttonsPressed.Clear();
		
		// Find the buttonGrid parent and broadcast reset to all circle scripts
		GameObject buttonGrid = GameObject.Find("buttonGrid");
		if (buttonGrid != null) {
			buttonGrid.BroadcastMessage("Reset", SendMessageOptions.DontRequireReceiver);
		}
	}
}
