using System.Collections.Generic;
using UnityEngine;

public class DestinationSetter : MonoBehaviour
{
    [SerializeField] private GameObject littleCircle;
    private Vector3 scale;
    private Vector3 circleVector;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private Highlighter highlighter;

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

        RaycastHit hitInfo = MouseRaycast.HitInfo;

        if (MouseRaycast.CurrentHitType == HitType.Ground)
        {

            circleVector = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);
            transform.position = circleVector;

            if (objects.Count < UnitManager.Instance.UnitsSelected.Count)
            {
                GameObject circle =
                Instantiate(
                    littleCircle,
                    new Vector3(Random.Range(-transform.localScale.x * 0.5f, transform.localScale.x * 0.5f), 0f, Random.Range(-transform.localScale.z * 0.5f, transform.localScale.z * 0.5f)) + transform.position,
                    new Quaternion(0f, 0f, 0f, 0f),
                    transform
                    );

                objects.Add(circle);
            }
        }

        if (highlighter.CurrentHighlight != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;

            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;

            foreach (GameObject obj in objects)
            {
                obj.SetActive(true);
            }
        }


        if (Input.GetMouseButtonUp(1))
        {
            for (int i = 0; i < UnitManager.Instance.UnitsSelected.Count; i++)
            {
                GameObject unit = UnitManager.Instance.UnitsSelected[i];

                switch (MouseRaycast.CurrentHitType)
                {
                    case HitType.Ground:
                        //Go to location
                        unit.GetComponent<BehaviourStateMachine>().SetAction(ActionType.Move, objects[i].transform.position);
                        break;

                    case HitType.Object:
                        //Collect Item
                        UnitManager.Instance.UnitsSelected[0].GetComponent<BehaviourStateMachine>().SetAction(ActionType.Move, highlighter.CurrentHighlight.transform.position);
                        break;

                    case HitType.Unit:
                        //Move to Ally location
                        unit.GetComponent<BehaviourStateMachine>().SetAction(ActionType.Move, hitInfo.transform.position);
                        break;

                    case HitType.Enemy:
                        //Attack Enemy
                        unit.GetComponent<BehaviourStateMachine>().SetAction(ActionType.Attack, hitInfo.collider.gameObject);
                        break;
                }
            }

            foreach (var dings in objects)
            {
                Destroy(dings.gameObject);
            }
            objects.Clear();
        }
    }
}