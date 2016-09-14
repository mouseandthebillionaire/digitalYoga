using UnityEngine;
using System.Collections;

public class PoseScript : MonoBehaviour {
	
	public Sprite			mountain;
	public Sprite[] 		poses;
	public Sprite[] 		badPoses;
	public static int 		p;
	public int				b;
	public SpriteRenderer	sr;
	
	public static 		PoseScript S;
	
	void Awake(){
		S = this;
		//sr = this.GetComponent<SpriteRenderer>();
		ChangePose ();
		//sr.sprite = mountain;
	}

	public void Mountain(){
		//sr.sprite = mountain;
	}
	
	public void Pose(){
		//sr.sprite = poses[p];
	}
	
	public void Nope(){
		b = (b+1) % badPoses.Length;
		//sr.sprite = badPoses[b];
		
	}
	
	public void ChangePose(){
		int _p = Random.Range(0, poses.Length);
		while (p == _p){
			_p = Random.Range(0, poses.Length);
		}
		p = _p;
	}
}
