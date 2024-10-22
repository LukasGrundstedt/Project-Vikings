using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour, ISelectable
{
    [field: SerializeField]
    public bool Selected { get; set; }

    [field: SerializeField]
    public GameObject SelectVisual { get; set; }

    [field: SerializeField]
    public NavMeshAgent EntityAgent { get; set; }

    [field: SerializeField]
    public Soldier SoldierStats { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
