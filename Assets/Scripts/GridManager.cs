using UnityEngine;

public class GridManager : MonoBehaviour {
    public int width { get; private set; }
    public int height { get; private set; }
    public float cellSize { get; private set; }
    public Vector2 origin { get; private set; }
    private bool drawGrid;

    private float topMargin;
    private float bottomMargin;
    private float leftMargin;
    private float rightMargin;

    public void Initialize(int width, int height, float cellSize, float topMargin, float bottomMargin, float leftMargin, float rightMargin) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.topMargin = topMargin;
        this.bottomMargin = bottomMargin;
        this.leftMargin = leftMargin;
        this.rightMargin = rightMargin;
        this.drawGrid = false;
        
        CalculateGridDimensions();
    }

    void Update() {
        // Handle grid toggle input
        if (Input.GetKeyDown(KeyCode.G)) {
            ToggleGrid();
        }

        // Draw grid if enabled
        if (drawGrid) {
            DrawGrid();
            DrawGridLabels();
        }
    }

    private void ToggleGrid() {
        drawGrid = !drawGrid;
    }

    void OnDrawGizmos() {
        if (drawGrid) {
            DrawGizmos();
        }
    }

    public void CalculateGridDimensions() {
        Camera cam = Camera.main;
        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;
        
        // Calculate available space after margins
        float availableWidth = screenWidth - (leftMargin + rightMargin);
        float availableHeight = screenHeight - (topMargin + bottomMargin);
        
        // Calculate cell size to fit the available space
        cellSize = Mathf.Min(availableWidth / width, availableHeight / height);
        
        // Calculate total grid dimensions
        float totalWidth = width * cellSize;
        float totalHeight = height * cellSize;
        
        // Calculate grid origin (top-left corner)
        origin = new Vector2(
            -screenWidth/2f + leftMargin + (availableWidth - totalWidth) / 2f,
            screenHeight/2f - topMargin - (availableHeight - totalHeight) / 2f
        );
    }

    // Convert row/col to world position (row 0, col 0 is top-left)
    public Vector3 GridToWorld(int row, int col) {
        return new Vector3(
            origin.x + (col + 0.5f) * cellSize,
            origin.y - (row + 0.5f) * cellSize,
            0f
        );
    }

    // Convert world position to row/col coordinates
    public Vector2Int WorldToGrid(Vector3 worldPos) {
        int col = Mathf.RoundToInt((worldPos.x - origin.x) / cellSize - 0.5f);
        int row = Mathf.RoundToInt((origin.y - worldPos.y) / cellSize - 0.5f);
        return new Vector2Int(row, col);
    }

    public void DrawGrid() {
        if (!drawGrid) return;

        // Draw grid bounds
        Camera cam = Camera.main;
        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;
        
        // Draw margin areas
        Color marginColor = new Color(0.5f, 0.5f, 0f, 0.2f);
        DrawMargins(screenWidth, screenHeight, marginColor);
        
        // Draw grid lines
        DrawGridLines();
    }

    private void DrawMargins(float screenWidth, float screenHeight, Color marginColor) {
        Debug.DrawLine(
            new Vector3(-screenWidth/2f, screenHeight/2f - topMargin, 0),
            new Vector3(screenWidth/2f, screenHeight/2f - topMargin, 0),
            marginColor
        );
        
        Debug.DrawLine(
            new Vector3(-screenWidth/2f, -screenHeight/2f + bottomMargin, 0),
            new Vector3(screenWidth/2f, -screenHeight/2f + bottomMargin, 0),
            marginColor
        );
        
        Debug.DrawLine(
            new Vector3(-screenWidth/2f + leftMargin, -screenHeight/2f, 0),
            new Vector3(-screenWidth/2f + leftMargin, screenHeight/2f, 0),
            marginColor
        );
        
        Debug.DrawLine(
            new Vector3(screenWidth/2f - rightMargin, -screenHeight/2f, 0),
            new Vector3(screenWidth/2f - rightMargin, screenHeight/2f, 0),
            marginColor
        );
    }

    private void DrawGridLines() {
        // Draw horizontal lines (rows)
        for (int row = 0; row <= height; row++) {
            Vector3 startPos = new Vector3(origin.x, origin.y - row * cellSize, 0);
            Vector3 endPos = new Vector3(origin.x + width * cellSize, origin.y - row * cellSize, 0);
            Debug.DrawLine(startPos, endPos, Color.yellow, Time.deltaTime);
        }

        // Draw vertical lines (columns)
        for (int col = 0; col <= width; col++) {
            Vector3 startPos = new Vector3(origin.x + col * cellSize, origin.y, 0);
            Vector3 endPos = new Vector3(origin.x + col * cellSize, origin.y - height * cellSize, 0);
            Debug.DrawLine(startPos, endPos, Color.yellow, Time.deltaTime);
        }
    }

    public void DrawGizmos() {
        if (!drawGrid) return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.2f); // Semi-transparent yellow
        for (int row = 0; row < height; row++) {
            for (int col = 0; col < width; col++) {
                Vector3 pos = GridToWorld(row, col);
                Gizmos.DrawWireCube(pos, new Vector3(cellSize, cellSize, 0.1f));
            }
        }
    }

    public void DrawGridLabels() {
        if (!drawGrid) return;

        for (int row = 0; row < height; row++) {
            for (int col = 0; col < width; col++) {
                Vector3 pos = GridToWorld(row, col);
                Debug.DrawLine(pos, pos, Color.white, Time.deltaTime);
            }
        }
    }
} 