using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private GameObject destinationPoint;

    private List<GameObject> drawnPoints;

    private GameObject currentPoints;

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

            currentPoints = selectable.PointParent;
        }

        if (currentPoints != null && !currentPoints.activeInHierarchy) currentPoints.SetActive(true);
    }
}