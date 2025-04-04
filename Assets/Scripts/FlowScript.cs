using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Add this for pointer checking

public class FlowScript : MonoBehaviour {

	/* 
	 * 
	 * A version of the ControlScript that focuses on Flow between poses
	 * Using a grid-based system for location placement
	 * 
	*/

	[Header("Grid Settings")]
	public int					gridWidth = 12;
	public int					gridHeight = 8;
	public float				cellSize = 1f;  // Size of each grid cell in world units

	[Header("Screen Edge Margins")]
	public float					topMargin = 1f;      // Space between top of screen and grid
	public float					bottomMargin = 1f;   // Space between bottom of screen and grid
	public float					leftMargin = 1f;     // Space between left of screen and grid
	public float					rightMargin = 1f;    // Space between right of screen and grid

	[Header("Location Settings")]
	public float					locationMoveTime = 1f;    // How long the movement takes

	public GameObject				b;
	private Transform				locationsParent;
	private List<GameObject>		activeLocations = new List<GameObject>();
	private GridManager			gridManager;

	public static FlowScript		S;

	void Awake(){
		S = this;
		
		// Add GridManager component and initialize it
		gridManager = gameObject.AddComponent<GridManager>();
		gridManager.Initialize(gridWidth, gridHeight, cellSize, 
							 topMargin, bottomMargin, leftMargin, rightMargin);
	}

	void Start () {
		locationsParent = GameObject.Find("locations").transform;
	}

	void Update() {
		// Test spawning with number keys 1-9
		for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++) {
			if (Input.GetKeyDown((KeyCode)i)) {
				PoseControl.S.SetPose(i - (int)KeyCode.Alpha0 - 1); // Convert keycode to pose index (0-based)
				NewPose();
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			Reset();
		}

		// Press M to move to next pose
		if(Input.GetKeyDown(KeyCode.M) && activeLocations.Count > 0){
			MovePose();
		}
	}

	public void Flow(){
		// Get PoseControl instance
		PoseControl poseControl = PoseControl.S;
		if (poseControl == null) {
			Debug.LogError("FlowScript: Cannot flow - PoseControl singleton not found!");
			return;
		}

		// Move to next pose
		int nextPose = (poseControl.currentPose + 1) % poseControl.GetPoseCount();
		poseControl.SetPose(nextPose);
		Debug.Log($"Flow: Moved to pose {nextPose} ({poseControl.poseName})");

		// Execute appropriate action based on pose type
		switch (poseControl.poseType.ToLower()) {
			case "new":
				Debug.Log("Flow: Creating new pose");
				NewPose();
				break;
			case "move":
				if (activeLocations.Count > 0) {
					Debug.Log("Flow: Moving locations to new positions");
					MovePose();
				} else {
					Debug.LogWarning("Flow: Cannot move - no active locations");
					NewPose(); // Fallback to creating new pose if no locations to move
				}
				break;
			case "modify":
				Debug.Log("Flow: Modifying current pose");
				ModifyPose();
				break;
			default:
				Debug.LogWarning($"Flow: Unknown pose type '{poseControl.poseType}', defaulting to new pose");
				NewPose();
				break;
		}
	}

	public void NewPose() {
		if (locationsParent == null) {
			Debug.LogError("FlowScript: Cannot spawn locations - locationsParent is not set!");
			return;
		}

		// Get current pose from PoseControl
		PoseControl poseControl = PoseControl.S;
		if (poseControl == null) {
			Debug.LogError("FlowScript: Cannot create pose - PoseControl singleton not found!");
			return;
		}

		// Clear existing locations
		Reset();

		// Get pose locations from PoseControl
		Vector2[] poseLocations = poseControl.poseLocation;
		if (poseLocations == null || poseLocations.Length == 0) {
			Debug.LogError("FlowScript: No pose locations defined in PoseControl!");
			return;
		}

		// Create new locations based on pose data
		for (int i = 0; i < poseLocations.Length; i++) {
			Vector2 poseLoc = poseLocations[i];
			
			// Convert pose location to row/col
			int row = Mathf.RoundToInt(poseLoc.y); // y coordinate maps to row
			int col = Mathf.RoundToInt(poseLoc.x); // x coordinate maps to column

			// Validate grid position
			if (row < 0 || row >= gridHeight || col < 0 || col >= gridWidth) {
				Debug.LogWarning($"Invalid pose location at index {i}: ({row}, {col}). Skipping.");
				continue;
			}

			// Create location at specified position
			GameObject go = GameObject.Instantiate(b) as GameObject;
			go.transform.SetParent(locationsParent);
			go.transform.position = gridManager.GridToWorld(row, col);
			go.name = i.ToString();
			
			activeLocations.Add(go);
			Debug.Log($"Created location {i} at row {row}, col {col}");
		}
	}

	void ModifyPose() {
		// Get current pose from PoseControl
		PoseControl poseControl = PoseControl.S;
		if (poseControl == null) {
			Debug.LogError("FlowScript: Cannot modify pose - PoseControl singleton not found!");
			return;
		}

		// Get current pose locations
		Vector2[] poseLocations = poseControl.poseLocation;
		if (poseLocations == null || poseLocations.Length == 0) {
			Debug.LogError("FlowScript: No pose locations defined in current pose!");
			return;
		}

		// Compare current active locations with pose locations
		if (activeLocations.Count < poseLocations.Length) {
			// Add a new location from the pose data
			for (int i = 0; i < poseLocations.Length; i++) {
				// Check if this pose location is already used
				bool locationExists = false;
				foreach (GameObject loc in activeLocations) {
					Vector3 pos = loc.transform.position;
					Vector2Int gridPos = gridManager.WorldToGrid(pos);
					Vector2 posePos = new Vector2(gridPos.y, gridPos.x); // Convert to pose format (col/row)
					
					if (posePos == poseLocations[i]) {
						locationExists = true;
						break;
					}
				}

				if (!locationExists) {
					Vector2 newPos = poseLocations[i];
					int row = Mathf.RoundToInt(newPos.y);
					int col = Mathf.RoundToInt(newPos.x);

					if (row >= 0 && row < gridHeight && col >= 0 && col < gridWidth) {
						GameObject go = GameObject.Instantiate(b) as GameObject;
						go.transform.SetParent(locationsParent);
						go.transform.position = gridManager.GridToWorld(row, col);
						go.name = activeLocations.Count.ToString();
						
						activeLocations.Add(go);
						Debug.Log($"Added new location at row {row}, col {col}");
						return; // Only add one location at a time
					}
				}
			}
		} else {
			// Remove a location that doesn't match any pose location
			for (int i = activeLocations.Count - 1; i >= 0; i--) {
				GameObject loc = activeLocations[i];
				Vector3 pos = loc.transform.position;
				Vector2Int gridPos = gridManager.WorldToGrid(pos);
				Vector2 posePos = new Vector2(gridPos.y, gridPos.x); // Convert to pose format (col/row)

				bool matchesAnyPoseLocation = false;
				foreach (Vector2 poseLocation in poseLocations) {
					if (posePos == poseLocation) {
						matchesAnyPoseLocation = true;
						break;
					}
				}

				if (!matchesAnyPoseLocation) {
					activeLocations.RemoveAt(i);
					Destroy(loc);
					Debug.Log($"Removed location at index {i}");
					return; // Only remove one location at a time
				}
			}
		}
	}

	void MovePose() {
		if (activeLocations.Count == 0) return;

		// Get PoseControl instance
		PoseControl poseControl = PoseControl.S;
		if (poseControl == null) {
			Debug.LogError("FlowScript: Cannot move locations - PoseControl singleton not found!");
			return;
		}

		// Get current pose locations
		Vector2[] currentPoseLocations = poseControl.poseLocation;
		if (currentPoseLocations == null || currentPoseLocations.Length == 0) {
			Debug.LogError("FlowScript: No pose locations defined in current pose!");
			return;
		}

		// Get previous pose locations
		int previousPoseIndex = (poseControl.currentPose - 1 + poseControl.GetPoseCount()) % poseControl.GetPoseCount();
		Vector2[] previousPoseLocations = poseControl.GetPoseLocations(previousPoseIndex);
		
		Debug.Log($"Moving from pose {previousPoseIndex} to pose {poseControl.currentPose}");

		// Find locations that have changed position
		for (int i = 0; i < Mathf.Min(previousPoseLocations.Length, activeLocations.Count); i++) {
			if (i >= currentPoseLocations.Length) break;

			Vector2 prevPos = previousPoseLocations[i];
			Vector2 newPos = currentPoseLocations[i];

			// If position has changed, move the location
			if (prevPos != newPos) {
				GameObject locationToMove = activeLocations[i];
				
				// Convert pose coordinates to grid coordinates
				int row = Mathf.RoundToInt(newPos.y);
				int col = Mathf.RoundToInt(newPos.x);

				// Validate new position
				if (row >= 0 && row < gridHeight && col >= 0 && col < gridWidth) {
					Vector3 targetPos = gridManager.GridToWorld(row, col);
					
					Debug.Log($"Moving location {i} from ({prevPos.x}, {prevPos.y}) to ({newPos.x}, {newPos.y})");
					StartCoroutine(MoveLocationCoroutine(locationToMove, targetPos));
				} else {
					Debug.LogWarning($"Invalid target position for location {i}: ({row}, {col})");
				}
			}
		}
	}

	IEnumerator MoveLocationCoroutine(GameObject location, Vector3 targetPos) {
		Vector3 startPos = location.transform.position;
		float elapsedTime = 0f;
		FingerScript fingerScript = location.GetComponent<FingerScript>();
		
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

		if (fingerScript != null) {
			fingerScript.OnLocationMoveComplete();
		}
	}

	public void Reset(){
		// Destroy all active locations
		foreach (GameObject button in activeLocations) {
			Destroy(button);
		}
		activeLocations.Clear();
		
		// Broadcast Reset message to all circle scripts
		locationsParent.BroadcastMessage("Reset", SendMessageOptions.DontRequireReceiver);
	}

}
