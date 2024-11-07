using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInteractor : MonoBehaviour
{
    [SerializeField] private GameObject destinationPoint;

    private List<GameObject> drawnPoints;

    private GameObject currentPoints;
    private GameObject tempObj = null;
    private GameObject tempUnit = null;

    // Start is called before the first frame update
    void Start()
    {
        drawnPoints = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoints != null && currentPoints.activeInHierarchy)
        {
            currentPoints.SetActive(false);
            currentPoints = null;
        }

        
        if (MouseRaycast.CurrentHitType == HitType.Object)
        {
            RaycastHit hitInfo = MouseRaycast.HitInfo;
            IInteractable selectable = hitInfo.collider.GetComponent<IInteractable>();

            if (Input.GetMouseButtonUp(1)) 
            { 
                tempObj = hitInfo.collider.gameObject;
                tempUnit = UnitManager.Instance.UnitsSelected[0];
            }

            currentPoints = selectable.PointParent;
        }

        if (tempUnit == null) return;

        Debug.Log(tempObj);
        Debug.Log(tempUnit);

        if (tempObj != null && tempUnit.transform.position.CompareDistance(tempObj.transform.position) < 0.5f) 
        {
            tempObj.transform.SetParent(tempUnit.transform, true);

            if (!tempUnit.GetComponentInChildren<MainHand>(true).MainHandFull)
            {
                tempObj.transform.position = tempUnit.GetComponentInChildren<MainHand>().transform.position;
                tempUnit.GetComponentInChildren<MainHand>().MainHandObj.SetActive(false);
                tempUnit.GetComponent<Unit>().CarriedMainObj = tempObj;
                tempUnit.GetComponentInChildren<MainHand>(true).MainHandFull = true;
                tempObj.layer = 0;
                tempObj = null;
                tempUnit = null;
            }
            else if (!tempUnit.GetComponentInChildren<OffHand>(true).OffHandFull)
            {
                tempObj.transform.position = tempUnit.GetComponentInChildren<OffHand>(true).transform.position;
                tempUnit.GetComponentInChildren<OffHand>(true).OffHandObj.SetActive(false);
                tempUnit.GetComponent<Unit>().CarriedOffObj = tempObj;
                tempUnit.GetComponentInChildren<OffHand>(true).OffHandFull = true;
                tempObj.layer = 0;
                tempObj = null;
                tempUnit = null;
            }
        }

        if (currentPoints != null && !currentPoints.activeInHierarchy) currentPoints.SetActive(true);
    }
}