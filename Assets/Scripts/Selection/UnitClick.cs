using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MouseRaycast.CurrentHitType == HitType.UI) return;
            if (MouseRaycast.CurrentHitType == HitType.Ground || MouseRaycast.CurrentHitType == HitType.None)
            {
                if (Input.GetKey(KeyCode.LeftShift)) return;
                UnitManager.Instance.DeselectAll();
            }
        }
    }

    public static void UnitClicked(GameObject unitToAdd)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            UnitManager.Instance.ShiftClickSelect(unitToAdd);
        }
        else
        {
            UnitManager.Instance.ClickSelect(unitToAdd);
        }
    }
}