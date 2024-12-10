using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    private Transform root;
    private Transform hand;

    [Tooltip("Adjusts the Shields distance to the Body while shielding")]
    [SerializeField] private float distanceToBody = 0.85f;

    // Start is called before the first frame update
    void Start()
    {
        root = transform.root;
        hand = transform.parent;
    }

    public void ShieldOpponent(GameObject opponent)
    {
        hand.position = root.position + (opponent.transform.position - root.position).normalized * distanceToBody;
        hand.forward = opponent.transform.position - root.position;
    }
}
