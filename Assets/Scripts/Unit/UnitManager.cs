using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance; /*{ get => instance == null ? new UnitManager() : instance; private set => instance = value; }
    private static UnitManager instance;*/

    public List<GameObject> Units = new();
    public List<GameObject> UnitsSelected = new();

    private void Awake()
    {
        if (Instance != this) Instance = this;

        Units = new List<GameObject>();
    }

    #region Selection
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        UnitsSelected.Add(unitToAdd);
        unitToAdd.GetComponent<ISelectable>().VisualizeSelection(true);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!UnitsSelected.Contains(unitToAdd))
        {
            UnitsSelected.Add(unitToAdd);
            unitToAdd.GetComponent<ISelectable>().VisualizeSelection(true);
        }
        else // This is throwing errors
        {
            unitToAdd.GetComponent<ISelectable>().VisualizeSelection(false);
            UnitsSelected.Remove(unitToAdd);
        }
    }

    public void DragClickSelect(GameObject unitToAdd)
    {
        if (!UnitsSelected.Contains(unitToAdd))
        {
            UnitsSelected.Add(unitToAdd);
            unitToAdd.GetComponent<ISelectable>().VisualizeSelection(true);
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject unitToRemove in UnitsSelected)
        {
            unitToRemove.GetComponent<ISelectable>().VisualizeSelection(false);
        }

        UnitsSelected.Clear();
    }

    #endregion
}
