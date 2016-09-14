using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {
	
	public static int 			t = 0;
	private float 				startTime = 0.0f;
	public bool					n;

	public static TimerScript	S;

	LineRenderer 				line;
	public Material 			mat;
	public float 				ThetaScale = 0.1f;
	private int					size;
	public float 				radius = 3f;
	private float 				Theta = 0f;

	private float				x;
	private float				y;

	public float				_i;

	public Color c1 = Color.white;
	public Color c2 = new Color(1, 1, 1, 0);

	
	// Use this for initialization
	void Start () {
		S = this;
		n = false;
		t = 0;
		startTime = Time.time;
		line = this.gameObject.AddComponent<LineRenderer>();

		line.material = mat;
		line.SetColors(c1, c2);
	}
	
	// Update is called once per frame
	void Update () {
		float timeElapsed = Time.time - startTime;
		t = 120 - Mathf.RoundToInt(timeElapsed);

		size = (int)((1f / ThetaScale) + 1f);
		line.SetVertexCount(size);
		Theta = 0f;

		for(int i = 0; i < size; i++){          
			Theta += ((timeElapsed * -.1f) * Mathf.PI * ThetaScale);         
			x = radius * Mathf.Cos(Theta);
			y = radius * Mathf.Sin(Theta);    
			line.SetPosition(i, new Vector3(x, y, 0));
		}

		if(Theta < -6.3f){
			Next();
			startTime = Time.time;
		}

	}
	
	public void Next() {
		PoseScript.S.ChangePose();
		buttonScript.S.Reset ();
		n = false;
	}

	
}