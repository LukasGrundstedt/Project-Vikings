using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SphereRenderer : MonoBehaviour
{
    [SerializeField] private LayerMask plane;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private GameObject littleCircle;
    private Vector3 scale;
    private Vector3 circleVector;
    [SerializeField] private List<GameObject> objects;


    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        objects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        scale = new Vector3(UnitManager.Instance.UnitsSelected.Count * 2f, UnitManager.Instance.UnitsSelected.Count * 2f, UnitManager.Instance.UnitsSelected.Count);

        transform.localScale = scale;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hitInfo, 100f, plane);

        circleVector = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);

        transform.position = circleVector;

        if (objects.Count < UnitManager.Instance.UnitsSelected.Count)
        {

            GameObject circle =
            Instantiate(
                littleCircle,
                new Vector3 (Random.Range(-transform.localScale.x * 0.5f, transform.localScale.x * 0.5f), 0f, Random.Range(-transform.localScale.z * 0.5f, transform.localScale.z * 0.5f)) + transform.position,
                new Quaternion(0f, 0f, 0f, 0f),
                transform
                );

            objects.Add(circle);
        }

        if (Input.GetMouseButtonUp(1))
        {
            for (int i = 0; i < UnitManager.Instance.UnitsSelected.Count; i++)
            {
                UnitManager.Instance.UnitsSelected[i].GetComponent<NavMeshAgent>().destination = objects[i].transform.position;
            }

            foreach (var dings in objects)
            {
                Destroy(dings.gameObject);
            }
            objects.Clear();
        }
    }
}
