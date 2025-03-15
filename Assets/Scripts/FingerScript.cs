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
	private bool                isPressed = false;
	private int                 trackingTouchId = -1;  // -1 means no touch being tracked

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
			if (!isPressed && collider.OverlapPoint(mouseWorldPos)) {
				isPressed = true;
				trackingTouchId = -1;  // Use -1 for mouse
				ring.GetComponent<RingScript>().Down();
				Debug.Log("Mouse Down");
			}
		}
		else if (Input.GetMouseButton(0)) {
			if (isPressed && trackingTouchId == -1) {  // Only check if we're tracking mouse
				if (!collider.OverlapPoint(mouseWorldPos)) {
					isPressed = false;
					ring.GetComponent<RingScript>().Up();
					Debug.Log("Mouse Exit While Moving");
				} else {
					Debug.Log("Mouse Still Pressed Over Location");
				}
			} else if (!isPressed && collider.OverlapPoint(mouseWorldPos)) {
				// Handle case where mouse entered location while held down
				isPressed = true;
				trackingTouchId = -1;
				ring.GetComponent<RingScript>().Down();
				Debug.Log("Mouse Entered While Held");
			}
		}
		else if (Input.GetMouseButtonUp(0)) {
			if (isPressed && trackingTouchId == -1) {  // Only check if we're tracking mouse
				isPressed = false;
				ring.GetComponent<RingScript>().Up();
				Debug.Log("Mouse Up");
			}
		}

		// Handle touch input
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.touches[i];
			Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);

			switch (touch.phase) {
				case TouchPhase.Began:
					if (!isPressed && collider.OverlapPoint(touchWorldPos)) {
						isPressed = true;
						trackingTouchId = touch.fingerId;
						ring.GetComponent<RingScript>().Down();
						Debug.Log("Touch Down");
					}
					break;

				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if (isPressed && touch.fingerId == trackingTouchId) {
						// If finger moved off the location while pressed, trigger Up
						if (!collider.OverlapPoint(touchWorldPos)) {
							isPressed = false;
							trackingTouchId = -1;
							ring.GetComponent<RingScript>().Up();
							Debug.Log("Touch Exit While Moving");
						} else {
							Debug.Log("Touch Still Over Location");
						}
					}
					break;

				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					if (isPressed && touch.fingerId == trackingTouchId) {
						isPressed = false;
						trackingTouchId = -1;
						ring.GetComponent<RingScript>().Up();
						Debug.Log("Touch Up");
					}
					break;
			}
		}
	}

	public void Reset(){
		//currColor = colors[0];
		isPressed = false;
		trackingTouchId = -1;
		ring.GetComponent<RingScript>().Up();
	}

	// These methods can stay for compatibility with existing code, but they'll be called less frequently
	public void Up() {
		if (isPressed) {
			isPressed = false;
			trackingTouchId = -1;
			ring.GetComponent<RingScript>().Up();
		}
	}

	public void OnLocationMoveStart() {
		// No need for special handling - Update will handle touch tracking
	}

	public void OnLocationMoveComplete() {
		// No need for special handling - Update will handle touch tracking
	}
}
