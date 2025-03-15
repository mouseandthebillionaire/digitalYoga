using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FingerScript : MonoBehaviour {

	public bool					on;
	public GameObject			b;
	//public Color[]			colors;
	//public Color				currColor;

	public GameObject			ring;
	private Collider2D          collider;
	private HashSet<int>        trackingTouchIds = new HashSet<int>();  // Track multiple touches
	private int                 activeTouchCount = 0;  // Count of active touches on this location

	static public FingerScript		S;

	// Use this for initialization
	void Start () {
		b = this.gameObject;
		on = false;
		//currColor = colors[0];
		collider = GetComponent<Collider2D>();
	}

	void Update() {
		// Handle mouse input for debugging
		Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if (Input.GetMouseButtonDown(0)) {
			if (collider.OverlapPoint(mouseWorldPos)) {
				activeTouchCount++;
				ring.GetComponent<RingScript>().Down();
				Debug.Log("Mouse Down");
			}
		}
		else if (Input.GetMouseButton(0)) {
			if (activeTouchCount > 0) {  // Only check if we're tracking mouse
				if (!collider.OverlapPoint(mouseWorldPos)) {
					activeTouchCount--;
					if (activeTouchCount == 0) {
						ring.GetComponent<RingScript>().Up();
					}
					Debug.Log("Mouse Exit While Moving");
				} else {
					Debug.Log("Mouse Still Pressed Over Location");
				}
			}
		}
		else if (Input.GetMouseButtonUp(0)) {
			if (activeTouchCount > 0) {  // Only check if we're tracking mouse
				activeTouchCount--;
				if (activeTouchCount == 0) {
					ring.GetComponent<RingScript>().Up();
				}
				Debug.Log("Mouse Up");
			}
		}

		// Handle touch input
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.touches[i];
			Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

			switch (touch.phase) {
				case TouchPhase.Began:
					if (collider.OverlapPoint(touchWorldPos)) {
						trackingTouchIds.Add(touch.fingerId);
						activeTouchCount++;
						ring.GetComponent<RingScript>().Down();
						Debug.Log($"Touch Down (ID: {touch.fingerId}, Total: {activeTouchCount})");
					}
					break;

				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if (trackingTouchIds.Contains(touch.fingerId)) {
						if (!collider.OverlapPoint(touchWorldPos)) {
							trackingTouchIds.Remove(touch.fingerId);
							activeTouchCount--;
							if (activeTouchCount == 0) {
								ring.GetComponent<RingScript>().Up();
							}
							Debug.Log($"Touch Exit While Moving (ID: {touch.fingerId}, Total: {activeTouchCount})");
						} else {
							Debug.Log($"Touch Still Over Location (ID: {touch.fingerId}, Total: {activeTouchCount})");
						}
					}
					break;

				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					if (trackingTouchIds.Contains(touch.fingerId)) {
						trackingTouchIds.Remove(touch.fingerId);
						activeTouchCount--;
						if (activeTouchCount == 0) {
							ring.GetComponent<RingScript>().Up();
						}
						Debug.Log($"Touch Up (ID: {touch.fingerId}, Total: {activeTouchCount})");
					}
					break;
			}
		}
	}

	public void Reset(){
		//currColor = colors[0];
		trackingTouchIds.Clear();
		activeTouchCount = 0;
		ring.GetComponent<RingScript>().Up();
	}

	// These methods can stay for compatibility with existing code, but they'll be called less frequently
	public void Up() {
		trackingTouchIds.Clear();
		activeTouchCount = 0;
		ring.GetComponent<RingScript>().Up();
	}

	public void OnLocationMoveStart() {
		// No special handling needed - Update will handle touch tracking
	}

	public void OnLocationMoveComplete() {
		// No special handling needed - Update will handle touch tracking
	}
}
