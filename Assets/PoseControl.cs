using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PoseControl : MonoBehaviour
{
    public int          currentPose;
    public string       poseName;
    public string       poseType; // New, Move, Modify
    public Vector2[]    poseLocation;

    public TextAsset	poseList;
    private string[]    poseNameArray;
    private string[]    poseTypeArray;
    private Vector2[][] poseLocationArray;


    public static PoseControl S;

    void Awake()
    {
        S = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPose = -1;
        if (poseList == null) {
            Debug.LogError("PoseControl: poseList TextAsset is not assigned in the Inspector!");
            return;
        }
        ParsePoseList();
        if (poseLocationArray == null || poseLocationArray.Length == 0) {
            Debug.LogError("PoseControl: No poses were loaded from poseList. Check the file format and contents.");
            return;
        }
        SetPose(currentPose);
        Debug.Log($"PoseControl: Successfully loaded {poseLocationArray.Length} poses.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ParsePoseList(){
        // Read all lines from the pose list file
        if (poseList == null) {
            Debug.LogError("PoseControl: poseList is not assigned!");
            return;
        }

        if (string.IsNullOrEmpty(poseList.text)) {
            Debug.LogError("PoseControl: poseList file is empty!");
            return;
        }

        string[] lines = poseList.text.Split('\n');
        Debug.Log($"PoseControl: Reading {lines.Length} lines from poseList");
        
        List<string> names = new List<string>();
        List<string> types = new List<string>();
        List<Vector2[]> locations = new List<Vector2[]>();

        foreach (string line in lines) {
            // Skip empty lines or comments
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//")) {
                continue;
            }

            // Split line into parts
            string[] parts = line.Split(',');
            if (parts.Length < 3) {
                Debug.LogWarning($"Invalid line format in poseList: {line}");
                continue;
            }

            string poseName = parts[0].Trim();
            string poseType = parts[1].Trim();

            // Create Vector2 array for locations
            Vector2[] poseLocations = new Vector2[parts.Length - 2];

            // Parse location pairs starting from index 2
            for (int i = 2; i < parts.Length; i++) {
                string[] coords = parts[i].Trim().Split('/');
                if (coords.Length == 2) {
                    float x, y;
                    if (float.TryParse(coords[0], out x) && float.TryParse(coords[1], out y)) {
                        poseLocations[i - 2] = new Vector2(x, y);
                        Debug.Log($"Parsed location for pose {poseName}: ({x}, {y})");
                    } else {
                        Debug.LogWarning($"Invalid coordinates in pose {poseName}: {parts[i]}");
                        poseLocations[i - 2] = Vector2.zero;
                    }
                } else {
                    Debug.LogWarning($"Invalid location format in pose {poseName}: {parts[i]}");
                    poseLocations[i - 2] = Vector2.zero;
                }
            }

            names.Add(poseName);
            types.Add(poseType);
            locations.Add(poseLocations);
            Debug.Log($"Added pose: {poseName} with {poseLocations.Length} locations");
        }

        // Convert lists to arrays
        poseNameArray = names.ToArray();
        poseTypeArray = types.ToArray();
        poseLocationArray = locations.ToArray();

        Debug.Log($"Parsed {poseNameArray.Length} poses from poseList with a total of {poseLocationArray.Sum(locations => locations.Length)} locations");
    }

    public void SetPose(int index){
        if (poseNameArray == null || poseTypeArray == null || poseLocationArray == null) {
            Debug.LogError("PoseControl: Pose arrays are not initialized!");
            return;
        }
        
        if (index < 0 || index >= poseNameArray.Length) {
            Debug.LogError($"PoseControl: Invalid pose index {index}. Valid range is 0-{poseNameArray.Length - 1}");
            return;
        }

        currentPose = index;
        poseName = poseNameArray[index];
        poseType = poseTypeArray[index];
        poseLocation = poseLocationArray[index];
        Debug.Log($"Set pose to {poseName} ({poseType}) with {poseLocation.Length} locations");
    }

    public Vector2[] GetPoseLocations(int index) {
        if (poseLocationArray == null) {
            Debug.LogError("PoseControl: Pose arrays are not initialized!");
            return null;
        }
        
        if (index < 0 || index >= poseLocationArray.Length) {
            Debug.LogError($"PoseControl: Invalid pose index {index}. Valid range is 0-{poseLocationArray.Length - 1}");
            return null;
        }

        return poseLocationArray[index];
    }

    public int GetPoseCount() {
        return poseLocationArray?.Length ?? 0;
    }
}
