using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblet : MonoBehaviour, IInteractable
{

    [field: SerializeField]
    public GameObject SelectVisual { get; set; }

    [field: SerializeField]
    public GameObject PointParent { get ; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
