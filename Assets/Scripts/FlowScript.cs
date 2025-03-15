using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this for pointer checking

public class FlowScript : MonoBehaviour {

	/* 
	 * 
	 * A version of the ControlScript that focses on Flow between poses
	 * 
	*/

	public float					topBuffer = 4f;      // World space buffer from top
	public float					bottomBuffer = -4f;   // World space buffer from bottom
	public float					leftBuffer = -7f;     // World space buffer from left
	public float					rightBuffer = 7f;     // World space buffer from right
	public float					minLocationDistance = 2f;  // Minimum distance between locations in world units
	public float					locationMoveDistance = 2f; // How far to move the location in world units
	public float					locationMoveTime = 1f;    // How long the movement takes
	public float					locationSize = 1f;        // Size of locations in world units

	public GameObject				b;
	private Transform				locationsParent;

	public List<string>				buttonsPressed = new List<string>();
	public static int				numRight;
	public static int				numPressed;

	private List<GameObject>		activeLocations = new List<GameObject>();
	public string[]					combo;

	//Audio
	public AudioSource				mallets, nope;
	public AudioClip[]				mNotes;

	public static FlowScript		S;

	void Awake(){
		S = this;
		numPressed = 0;
	}

	void Start () {
		locationsParent = GameObject.Find("locations").transform;
	}

	void Update() {
		// Test spawning with number keys 1-9
		for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++) {
			if (Input.GetKeyDown((KeyCode)i)) {
				NewPose(i - (int)KeyCode.Alpha0); // Convert keycode to number (1-9)
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			Reset();
		}

		// Press M to move a random location
		if(Input.GetKeyDown(KeyCode.M) && activeLocations.Count > 0){
			MoveRandomLocation();
		}
	}

	public void Flow(){
		// 50/50 chance between spawning new pose or moving existing location
		if (Random.value < 0.4f) {
			// Generate random count between 1 and 5
			int count = Random.Range(1, 6); // Range is inclusive of min, exclusive of max
			Debug.Log($"Flow: Creating new pose with {count} locations");
			NewPose(count);
		} else {
			Debug.Log("Flow: Moving random location");
			MoveRandomLocation();
		}
	}

	public void NewPose(int count) {
		Debug.Log("NewPose called with count: " + count);
		if (locationsParent == null) {
			Debug.LogError("FlowScript: Cannot spawn locations - locationsParent is not set!");
			return;
		}

		// Clear existing locations before spawning new ones
		Reset();

		int maxAttempts = 50; // Maximum attempts to find a valid position

		for (int i = 0; i < count; i++) {
			GameObject go = GameObject.Instantiate(b) as GameObject;
			Vector3 pos = Vector3.zero;
			bool validPosition = false;
			int attempts = 0;

			// Keep trying positions until we find one that's far enough from other locations
			while (!validPosition && attempts < maxAttempts) {
				// Calculate random position within world space bounds
				float xPos = Random.Range(leftBuffer, rightBuffer);
				float yPos = Random.Range(bottomBuffer, topBuffer);
				pos = new Vector3(xPos, yPos, 0);
				validPosition = true;

				// Check distance from all existing locations
				foreach (GameObject existingLocation in activeLocations) {
					if (Vector3.Distance(existingLocation.transform.position, pos) < minLocationDistance) {
						validPosition = false;
						break;
					}
				}
				attempts++;
			}

			go.transform.SetParent(locationsParent);
			go.transform.position = pos;
			go.transform.localScale = Vector3.one * locationSize;

			go.name = i.ToString();
			activeLocations.Add(go);

			if (attempts >= maxAttempts) {
				Debug.LogWarning("Could not find position with minimum distance after " + maxAttempts + " attempts for location " + i);
			}
		}
	}

	public void Add(string buttonNum){
		numPressed += 1;
		combo = ComboScript.S.currCombo;
		buttonsPressed.Add(buttonNum);

		// Find and add the pressed button to activeLocations
		GameObject pressedButton = GameObject.Find(buttonNum);
		if (pressedButton != null) {
			activeLocations.Add(pressedButton);
		}

		if (System.Array.IndexOf(combo, buttonNum) != -1) {
			numRight++;
			mallets.PlayOneShot(mNotes[numRight]);
		} else {
			nope.Play();
		}
		if (numPressed == combo.Length) {
			if (numRight == combo.Length) {
				// Increase the score
				ScoreScript.S.Score(1);
			} else {
				// Decrease the score!
				ScoreScript.S.Score(-1); 
			}
		}
	}

	public void Remove(string buttonNum){
		if(System.Array.IndexOf(combo, buttonNum) != -1){
			if(numRight > 0) numRight--;
		}
		if (numPressed > 0) {
			numPressed -= 1;
			buttonsPressed.Remove(buttonNum);
		}
	}

	public void Reset(){
		numRight = 0;
		numPressed = 0;
		buttonsPressed.Clear();
		
		// Destroy all active locations
		foreach (GameObject button in activeLocations) {
			Destroy(button);
		}
		activeLocations.Clear();
		
		// Broadcast Reset message to all circle scripts
		locationsParent.BroadcastMessage("Reset", SendMessageOptions.DontRequireReceiver);
	}

	void MoveRandomLocation() {
		if (activeLocations.Count == 0) return;

		// Select a random location
		int randomIndex = Random.Range(0, activeLocations.Count);
		GameObject locationToMove = activeLocations[randomIndex];
		
		// Start the movement coroutine
		StartCoroutine(MoveLocationCoroutine(locationToMove));
	}

	IEnumerator MoveLocationCoroutine(GameObject location) {
		Vector3 startPos = location.transform.position;
		
		// Choose a random direction
		float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		Vector3 moveDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0).normalized;
		Vector3 targetPos = startPos + (moveDirection * locationMoveDistance);

		// Clamp target position to stay within world space bounds
		targetPos.x = Mathf.Clamp(targetPos.x, leftBuffer, rightBuffer);
		targetPos.y = Mathf.Clamp(targetPos.y, bottomBuffer, topBuffer);

		float elapsedTime = 0f;
		FingerScript fingerScript = location.GetComponent<FingerScript>();
		
		// Notify FingerScript that movement is starting
		if (fingerScript != null) {
			fingerScript.OnLocationMoveStart();
		}
		
		while (elapsedTime < locationMoveTime) {
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / locationMoveTime;
			
			// Use smooth step for more natural movement
			t = t * t * (3f - 2f * t);
			
			location.transform.position = Vector3.Lerp(startPos, targetPos, t);
			yield return null;
		}

		// Ensure we end up exactly at the target position
		location.transform.position = targetPos;

		// Notify FingerScript that movement is complete
		if (fingerScript != null) {
			fingerScript.OnLocationMoveComplete();
		}
	}

}
