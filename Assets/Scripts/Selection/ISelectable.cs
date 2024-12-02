using UnityEngine;

public interface ISelectable : IHighlightable
{
    public bool Selected { get; set; }

    public virtual void VisualizeSelection(bool isSelected)
    {
        Highlighted = isSelected;
        SelectVisual.SetActive(isSelected);
    }
}