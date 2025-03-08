using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	public TextAsset	poseList;
	public string[]		poses;

	public Text			time;
	public Text			pose;
	public Text			score;

	// Use this for initialization
	void Start () {
		poses = poseList.text.Split ('\n');
	
	}
	
	// Update is called once per frame
	void Update () {
		int p = ComboScript.c;
		int l = ComboScript.currComboLength;
		pose.text = "Current Pose: "+poses[p]+ " ("+ l +")";
	}
}
