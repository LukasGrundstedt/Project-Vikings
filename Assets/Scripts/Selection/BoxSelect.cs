using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSelect : MonoBehaviour
{
    [SerializeField] private RectTransform SelectionBox;

    private Vector2 startPoint;
    private Vector2 endPoint;
    Vector2 boxCenter;
    private Vector2 boxSize;
    private Vector2 boxOffset;

    private Rect selectionBox;

    [SerializeField] LayerMask selectLayer;

    [SerializeField] private List<GameObject> selectedObjects = new();
    [SerializeField] private List<GameObject> previousSelection = new();

    [SerializeField] private List<GameObject> currentHighlights = new List<GameObject>();
    [SerializeField] private List<GameObject> previousHighlights = new List<GameObject>();

    [SerializeField] private GameObject debugSphere;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = Vector2.zero;
        endPoint = Vector2.zero;
        DrawBox();
    }

    // Update is called once per frame
    void Update()
    {
        SelectionInput();
    }

    private void SelectionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
            selectionBox = new();
        }

        if (Input.GetMouseButton(0))
        {
            endPoint = Input.mousePosition;

            DrawBox();

            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();

            startPoint = Vector2.zero;
            endPoint = Vector2.zero;
            DrawBox();
        }
    }

    private void DrawBox()
    {
        boxCenter = (startPoint + endPoint) * 0.5f;
        SelectionBox.position = boxCenter;

        boxSize = new (Mathf.Abs(startPoint.x - endPoint.x), Mathf.Abs(startPoint.y - endPoint.y));

        SelectionBox.sizeDelta = boxSize;

        
    }

    private void DrawSelection()
    {
        if (Input.mousePosition.x < startPoint.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPoint.x;
        }
        else
        {
            selectionBox.xMin = startPoint.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < startPoint.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPoint.y;
        }
        else
        {
            selectionBox.yMin = startPoint.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    private void SelectUnits()
    {
        foreach (GameObject unit in UnitManager.Instance.Units)
        {
            if (selectionBox.Contains(Camera.main.WorldToScreenPoint(unit.transform.position)))
            {
                UnitManager.Instance.DragClickSelect(unit);
            }
        }
    }
}
