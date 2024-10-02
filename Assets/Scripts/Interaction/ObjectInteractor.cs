using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private GameObject destinationPoint;

    private List<GameObject> drawnPoints;

    private GameObject currentPoints;
    private GameObject tempObj;
    private bool rightHandfull = false;

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

        if (UnitManager.Instance.UnitsSelected.Count <= 0) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, interactableMask))
        {
            IInteractable selectable = hitInfo.collider.GetComponent<IInteractable>();

            tempObj = hitInfo.collider.gameObject;

            currentPoints = selectable.PointParent;

        }

        if (tempObj != null && UnitManager.Instance.UnitsSelected[0].transform.position.magnitude - tempObj.transform.position.magnitude < 0.5f && UnitManager.Instance.UnitsSelected[0].transform.position.magnitude - tempObj.transform.position.magnitude > -0.5f) 
        {
            tempObj.transform.SetParent(UnitManager.Instance.UnitsSelected[0].transform, true);

            if (!rightHandfull)
            {
                tempObj.transform.position = UnitManager.Instance.UnitsSelected[0].GetComponentInChildren<RightHand>().transform.position;
                UnitManager.Instance.UnitsSelected[0].GetComponentInChildren<RightHand>().RightHandObj.SetActive(false);
                rightHandfull = true;
                tempObj.layer = 0;
                tempObj = null;
            }
            else
            {
                tempObj.transform.position = UnitManager.Instance.UnitsSelected[0].GetComponentInChildren<LeftHand>().transform.position;
                UnitManager.Instance.UnitsSelected[0].GetComponentInChildren<LeftHand>().LeftHandObj.SetActive(false);
                tempObj.layer = 0;
                tempObj = null;
            }
        }

        if (currentPoints != null && !currentPoints.activeInHierarchy) currentPoints.SetActive(true);
    }
}
