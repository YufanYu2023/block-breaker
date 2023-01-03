using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> mousePositions;

    [SerializeField] float lineWidth = 0.1f;
    [SerializeField] Color startColor = Color.blue;
    [SerializeField] Color endColor = Color.red;
    [SerializeField] float xMin = 1f;
    [SerializeField] float xMax = 15f;
    [SerializeField] float yMin = 1f;
    [SerializeField] float yMax = 12f;
    [SerializeField] float ScreenWidthInUnits = 16f;
    [SerializeField] float ScreenHeightInUnits = 12f;
    [SerializeField] float destroyTime = 2f;


    private void Update()
    {
        Vector2 linePods = new Vector2(transform.position.x, transform.position.y);
        linePods.x = Mathf.Clamp(GetXPos(), xMin, xMax);
        linePods.x = Mathf.Clamp(GetYPos(), yMin, yMax);

        if (Input.GetMouseButtonDown(0))
        {
            CreatLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempMousePos, mousePositions[mousePositions.Count - 1]) > 0.1f)
            {
                UpdateLine(tempMousePos);
                Destroy(currentLine, destroyTime);
            }

        }
    }

    private void CreatLine()
    {
        currentLine = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.startColor = startColor; lineRenderer.endColor = endColor;
        lineRenderer.startWidth = lineWidth; lineRenderer.endWidth = lineWidth;

        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        mousePositions.Clear();
        mousePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0,mousePositions[0]);
        lineRenderer.SetPosition(1, mousePositions[1]);
        edgeCollider.points = mousePositions.ToArray();
    }

    private void UpdateLine(Vector2 newMousePos)
    {
        mousePositions.Add(newMousePos);
        if (lineRenderer)
        {
            lineRenderer.positionCount++;
        }
        lineRenderer.SetPosition(lineRenderer.positionCount-1,newMousePos);
        edgeCollider.points = mousePositions.ToArray();
    }

    private float GetXPos()
    {
        return Input.mousePosition.x / Screen.width * ScreenWidthInUnits;
    }

    private float GetYPos()
    {
        return Input.mousePosition.y / Screen.width * ScreenHeightInUnits;
    }

}
