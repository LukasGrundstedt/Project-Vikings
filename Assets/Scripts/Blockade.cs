using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour, IDestructable
{
    [field: SerializeField]
    public float HitPoints { get; set; }

    [field: SerializeField]
    public float Toughness { get; set; }

    [field: SerializeField]
    public AnimationClip DestructionAnimation { get; set; }

    [field: SerializeField]
    public bool IsDestroyed { get; set; }

    public void OnDestruction()
    {
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
