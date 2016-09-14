using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ScoreScript : MonoBehaviour {

	private float				s;

	LineRenderer 				sLine;
	public Material 			mat;
	public float 				ThetaScale = 0.1f;
	private int					size;
	public float 				radius = 3f;
	private float 				Theta = 0f;

	private int					state;

	// Use this for initialization
	void Start () {
		sLine = this.gameObject.AddComponent<LineRenderer>();
		state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//int state = ControlScript.state;
		if(Input.GetKey("up")) state = 1;
		if(Input.GetKey("down")) state = 2;

		sLine.material = mat;

		size = (int)((1f / ThetaScale) + 1f);
		sLine.SetVertexCount(size);
		Theta = 0f;

		for(int i = 0; i < size; i++){          
			Theta += ((s * -.1f) * Mathf.PI * ThetaScale);         
			float x = radius * Mathf.Cos(Theta);
			float y = radius * Mathf.Sin(Theta);          
			sLine.SetPosition(i, new Vector3(x, y, 0));
		}

		if(state == 1) s += 0.01f;
		else if(state == 2) s -= 0.01f;
		else s += 0;

		if(s > 20) SceneManager.LoadScene("GameOver");
	}
}
