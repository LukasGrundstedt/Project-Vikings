using UnityEngine;

public interface ISelectable : IHighlightable
{
    public virtual void VisualizeSelection(bool isSelected)
    {
        SelectVisual.SetActive(isSelected);
    }
}
