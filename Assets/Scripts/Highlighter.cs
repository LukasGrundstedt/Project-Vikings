using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private LayerMask highlightMask;

    [SerializeField] private List<GameObject> currentHighlights = new List<GameObject>();
    [SerializeField] private List<GameObject> previousHighlights = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}