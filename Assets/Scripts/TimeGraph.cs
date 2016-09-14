// Unused
//-----------

using UnityEngine;
using System.Collections;

public class TimeGraph : MonoBehaviour {

	// Time Graph 
	public float 		ThetaScale = 0.01f;
	public float 		radius = 3f;
	private int 		Size;
	private float		time = 2f;
	private float 		Theta = 0f;

	LineRenderer 		line;

	// Use this for initialization
	void Start () {
		// Time Graph
		line = this.gameObject.AddComponent<LineRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
		Theta = 0f;
		if(time <= 0){
			time = 2f;
		} else {
			time -= .00185f;
		}

		Debug.Log(time);
		Size = (int)((1f / ThetaScale) + 1f);
		line.SetVertexCount(Size); 
		for(int i = 0; i < Size; i++){          
			Theta += (time * Mathf.PI * ThetaScale);         
			float x = radius * Mathf.Cos(Theta);
			float y = radius * Mathf.Sin(Theta);          
			line.SetPosition(i, new Vector3(x, y, 0));
		}
	
	}
}
