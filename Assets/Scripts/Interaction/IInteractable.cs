using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable : IHighlightable
{
    public GameObject PointParent { get; set; }
}