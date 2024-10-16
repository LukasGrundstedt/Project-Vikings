using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MouseRaycast.CurrentHitType == HitType.Unit)
            {
                RaycastHit hitInfo = MouseRaycast.HitInfo;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitManager.Instance.ShiftClickSelect(hitInfo.collider.gameObject);
                }
                else
                {
                    UnitManager.Instance.ClickSelect(hitInfo.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift)) UnitManager.Instance.DeselectAll();
            }
        }
    }
}
