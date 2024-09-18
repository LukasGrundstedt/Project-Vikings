using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private LayerMask clickable;
    [SerializeField] private LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo, 100f, clickable))
            {

            }
            else
            {
                UnitManager.Instance.DeselectAll();
            }
        }
    }
}
