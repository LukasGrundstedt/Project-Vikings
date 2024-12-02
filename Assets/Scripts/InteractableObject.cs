using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : Entity, IInteractable
{
    [field: SerializeField]
    public GameObject PointParent { get ; set; }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
