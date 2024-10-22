using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public List<GameObject> Units = new();
    public List<GameObject> UnitsSelected = new();

    private void Awake()
    {
        Instance = this;

        Units = new List<GameObject>();
    }


    #region Selection
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        UnitsSelected.Add(unitToAdd);
        Highlight(unitToAdd, true);
        unitToAdd.GetComponent<ISelectable>().Selected = true;
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!UnitsSelected.Contains(unitToAdd))
        {
            UnitsSelected.Add(unitToAdd);
            Highlight(unitToAdd, true);
            unitToAdd.GetComponent<ISelectable>().Selected = true;
        }
        else // This is throwing errors
        {
            unitToAdd.GetComponent<ISelectable>().Selected = false;
            UnitsSelected.Remove(unitToAdd);
            Highlight(unitToAdd, false);
        }
    }

    public void DragClickSelect(GameObject unitToAdd)
    {
        if (!UnitsSelected.Contains(unitToAdd))
        {
            UnitsSelected.Add(unitToAdd);
            Highlight(unitToAdd, true);
            unitToAdd.GetComponent<ISelectable>().Selected = true;
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject unitToRemove in UnitsSelected)
        {
            Highlight(unitToRemove, false);
            unitToRemove.GetComponent<ISelectable>().Selected = false;
        }

        UnitsSelected.Clear();
    }

    private void Highlight(GameObject obj, bool b)
    {
        obj.GetComponent<ISelectable>().VisualizeSelection(b);
    }
    #endregion
}
