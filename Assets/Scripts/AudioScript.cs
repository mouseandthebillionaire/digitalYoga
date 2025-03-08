using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class AudioScript : MonoBehaviour {

	// Don't forget to make this sound better
    
	//Audio
	public AudioSource				yup;
	public AudioClip[]				yMelodies;
	public AudioSource				vocal;
	public AudioClip[]				poseIntros;

	//public AudioClip[]              poseNames;
	public string[]                 poseNames;
	public string                   thisPose;

	public AudioMixerSnapshot[] 	snapshots;
	public float					t_time;
	private int						yL;
	private int						state;
	private int						t;

	private AudioClip				m;

	public static					AudioScript S;

	// Use this for initialization
	void Awake () {
		S = this;
		t = 0;
		state = 0;
	}

	void Update() {
		state = GameManagerScript.S.poseState;
		yL = ComboScript.currPose;

		switch(state){
		case 0:
			StartCoroutine(Return());
			break;
		case 1:
			StartCoroutine(Yup());
			break;
		default:
			StartCoroutine(Return());
			break;
		}
	}

	public void Welcome(){
		Debug.Log ("Welcome to Digital Yoga");
	}
    
	public void IntroducePose(int poseNum){
		Debug.Log(poseNum);
		//thisPose = poseNames[poseNum];
		StartCoroutine(PoseIntro());
		Debug.Log ("And now, moving on to...");
	}
    
	private IEnumerator PoseIntro(){
		int i = Random.Range(0, poseIntros.Length);
		vocal.PlayOneShot(poseIntros[i]);
		while (vocal.isPlaying){
			yield return null;
		}
		Debug.Log(thisPose);
		yield break;
	}

	public void Interjection(){
		Debug.Log ("Exhale and release into the pose");
	}



	private IEnumerator Yup() {
		yup.PlayOneShot(yMelodies[yL], 0.5f);
		snapshots[state].TransitionTo(0.5f);
		yield break;
	}

	private IEnumerator Nope() {
		//snapshots[state].TransitionTo(5f);
		yield return new WaitForSeconds(1f);
		yup.Stop();
		yield break;
	}

	private IEnumerator Return(){
		snapshots[state].TransitionTo(3f);
		yield return new WaitForSeconds(1f);
		yup.Stop();
		yield break;
	}
}
