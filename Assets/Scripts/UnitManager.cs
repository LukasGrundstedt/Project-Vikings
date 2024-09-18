using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public List<GameObject> Units = new();
    public List<GameObject> UnitsSelected = new();

    private void Awake()
    {
        Instance = this;
<<<<<<< Updated upstream

        Units = new List<GameObject>();
  
        Units = GameObject.FindGameObjectsWithTag("Unit").ToList();
=======
>>>>>>> Stashed changes
    }

    public void ClickSelect(GameObject unitToAdd)
    {

    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {

    }

    public void DragClickSelect(GameObject unitToAdd)
    {

    }

    public void DeselectAll()
    {

    }

    public void Deselect(GameObject unitToDeselct)
    {

    }
}
