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

    [SerializeField] LayerMask selectLayer;

    [SerializeField] private List<GameObject> selectedObjects = new();
    [SerializeField] private List<GameObject> previousSelection = new();

    [SerializeField] private List<GameObject> currentHighlights = new List<GameObject>();
    [SerializeField] private List<GameObject> previousHighlights = new List<GameObject>();

    [SerializeField] private GameObject debugSphere;

    private float zHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = Vector2.zero;
        endPoint = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateBox3D();

        RenderBoxImage();

        HighlightObjects();
    }

    private void HighlightObjects()
    {
        previousHighlights = new(currentHighlights);
        currentHighlights.Clear();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, 20f, selectLayer);
        if (hitInfo.collider != null) currentHighlights.Add(hitInfo.collider.gameObject);

        foreach (GameObject highlight in previousHighlights)
        {
            highlight.GetComponent<ISelectable>().VisualizeSelection(currentHighlights.Contains(highlight));
        }
    }

    private void CalculateBox3D()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endPoint = Input.mousePosition;

            RenderBoxImage();
        }

        if (Input.GetMouseButtonUp(0))
        {
            startPoint = Vector2.zero;
            endPoint = Vector2.zero;
            RenderBoxImage();

            GetSelection3D();
            UpdateHighlights();
        }
    }

    private void RenderBoxImage()
    {
        boxCenter = (startPoint + endPoint) * 0.5f;
        SelectionBox.position = boxCenter;

        boxSize = new (Mathf.Abs(startPoint.x - endPoint.x), Mathf.Abs(startPoint.y - endPoint.y));

        SelectionBox.sizeDelta = boxSize;
        Debug.Log(SelectionBox.rect);
    }

    private void GetSelection3D()
    {
        selectedObjects.Clear();

        foreach (var unit in UnitManager.Instance.Units)
        {
            Debug.Log(Camera.main.WorldToScreenPoint(unit.transform.position));
            if (SelectionBox.rect.Contains(Camera.main.WorldToScreenPoint(unit.transform.position)))
            {
                selectedObjects.Add(unit.gameObject);
            }
        }
    }

    private void UpdateHighlights()
    {
        foreach (var unit in UnitManager.Instance.Units)
        {
            unit.GetComponent<ISelectable>().VisualizeSelection(selectedObjects.Contains(unit));
        }
    }
}
