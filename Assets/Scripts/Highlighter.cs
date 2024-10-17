using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    private GameObject currentHighlight;

    private RaycastHit hitInfo;

    public GameObject CurrentHighlight { get => currentHighlight; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // DeHighlight Objects
        if (currentHighlight != null && currentHighlight.activeInHierarchy)
        {
            bool isSelected = false;

            if (hitInfo.collider.TryGetComponent<ISelectable>(out ISelectable selectable))
            {
                isSelected = selectable.Selected;
            }

            currentHighlight.SetActive(isSelected);
            currentHighlight = null;
        }

        // Get Highlightable Objects
        if (MouseRaycast.CurrentHitType == HitType.Unit || MouseRaycast.CurrentHitType == HitType.Object)
        {
            hitInfo = MouseRaycast.HitInfo;

            currentHighlight = hitInfo.collider.GetComponent<IHighlightable>().SelectVisual;
        }

        // Highlight Object
        if (currentHighlight != null && !currentHighlight.activeInHierarchy) currentHighlight.SetActive(true);
    }
}