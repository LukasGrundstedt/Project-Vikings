using UnityEngine;

public interface ISelectable
{
    public GameObject SelectVisual { get; set; }

    public virtual void VisualizeSelection(bool isSelected)
    {
        SelectVisual.SetActive(isSelected);
    }
}
