using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleGridScript : MonoBehaviour {

	public int						rows, columns;
	private float					viewportWidth, viewportHeight;
	private float					horizontalSpacing, verticalSpacing;
	public float					horizontalMargin = 1f; // Margin in Unity units
	public float					verticalMargin = 1f;   // Margin in Unity units
	
	// Circle
	public GameObject				c;
	private Transform				circleTransform;

	public GameObject[]				circles;

	public static circleGridScript S;

	// Use this for initialization
	void Start () {
		S = this;
		InitializeGrid();
	}

	void InitializeGrid() {
		Camera cam = Camera.main;
		viewportHeight = cam.orthographicSize * 2f;
		viewportWidth = viewportHeight * cam.aspect;

		// Calculate usable area
		float usableWidth = viewportWidth - (2f * horizontalMargin);
		float usableHeight = viewportHeight - (2f * verticalMargin);

		// Calculate spacing
		horizontalSpacing = usableWidth / (columns - 1);
		verticalSpacing = usableHeight / (rows - 1);

		circles = new GameObject[columns * rows];
		circleTransform = GameObject.Find("CircleGrid").transform;

		// Calculate horizontal start (leftmost position)
		float startX = -viewportWidth/2f + horizontalMargin;
		
		// Calculate vertical center and start position
		float totalGridHeight = verticalSpacing * (rows - 1);
		float startY = totalGridHeight / 2f; // Start from half the grid height (for centering)

		for(int i = 0; i < columns; i++) {
			for(int j = 0; j < rows; j++) {
				GameObject go = GameObject.Instantiate(c) as GameObject;
				
				float xPos = startX + (i * horizontalSpacing);
				float yPos = startY - (j * verticalSpacing); // Move down from top
				Vector3 pos = new Vector3(xPos, yPos, 0f);

				go.transform.SetParent(circleTransform);
				go.transform.position = pos;

				int index = i * rows + j;
				go.name = index.ToString();
				circles[index] = go;
			}
		}
	}

	// Call this method when viewport size changes
	public void UpdateGridPositions() {
		if (circles == null || circles.Length == 0) return;

		Camera cam = Camera.main;
		viewportHeight = cam.orthographicSize * 2f;
		viewportWidth = viewportHeight * cam.aspect;

		float usableWidth = viewportWidth - (2f * horizontalMargin);
		float usableHeight = viewportHeight - (2f * verticalMargin);

		horizontalSpacing = usableWidth / (columns - 1);
		verticalSpacing = usableHeight / (rows - 1);

		float startX = -viewportWidth/2f + horizontalMargin;
		float totalGridHeight = verticalSpacing * (rows - 1);
		float startY = totalGridHeight / 2f;

		for(int i = 0; i < columns; i++) {
			for(int j = 0; j < rows; j++) {
				int index = i * rows + j;
				if (circles[index] != null) {
					float xPos = startX + (i * horizontalSpacing);
					float yPos = startY - (j * verticalSpacing);
					circles[index].transform.position = new Vector3(xPos, yPos, 0f);
				}
			}
		}
	}

	public void Press(string s){
		int i = int.Parse(s);
		circles[i].GetComponent<circleScript>().Down();
	}

	public void Wrong(string s){
		int i = int.Parse(s);
		circles[i].GetComponent<circleScript>().Wrong();
	}

	public void Unpress(string s){
		int i = int.Parse(s);
		circles[i].GetComponent<circleScript>().Up();
	}

	public void Reset(){
		for (int i = 0; i < circles.Length; i++) {
			circles[i].GetComponent<circleScript>().Reset();
		}
	}
		
}
