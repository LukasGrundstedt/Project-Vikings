using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHighlightable
{
    public bool Highlighted { get; set; }
    public GameObject SelectVisual { get; set; }

    public void Highlight(bool highlighted);
}