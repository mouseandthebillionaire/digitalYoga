using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    private Vector2 lastScreenSize;
    private circleGridScript gridScript;

    void Start()
    {
        lastScreenSize = new Vector2(Screen.width, Screen.height);
        gridScript = FindObjectOfType<circleGridScript>();
    }

    void Update()
    {
        // Check if screen size has changed
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
        {
            lastScreenSize = new Vector2(Screen.width, Screen.height);
            if (gridScript != null)
            {
                gridScript.UpdateGridPositions();
            }
        }
    }
} 