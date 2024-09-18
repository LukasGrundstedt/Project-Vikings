using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private LayerMask highlightMask;

    [SerializeField] private List<GameObject> currentHightlights = new List<GameObject>();
    [SerializeField] private List<GameObject> previousHightlights = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, 20f, highlightMask);
        currentHightlights.Add(hitInfo.collider.gameObject);

        foreach (GameObject highlight in currentHightlights)
        {
            highlight.GetComponent<ISelectable>().VisualizeSelection(currentHightlights.Contains(highlight));
        }
    }
}
