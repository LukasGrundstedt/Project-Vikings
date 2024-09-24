using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [Tooltip("Choose 'Interactable', since it derives from IHighlightable")]
    [SerializeField] private LayerMask highlightMask;

    private GameObject currentHighlight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHighlight != null && currentHighlight.activeInHierarchy)
        {
            currentHighlight.SetActive(false);
            currentHighlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, highlightMask))
        {
            currentHighlight = hitInfo.collider.GetComponent<IHighlightable>().SelectVisual;
        }

        if (currentHighlight != null && !currentHighlight.activeInHierarchy) currentHighlight.SetActive(true);
    }
}