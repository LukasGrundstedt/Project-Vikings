using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        UnitManager.Instance.Units.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitManager.Instance.Units.Remove(this.gameObject);
    }
}