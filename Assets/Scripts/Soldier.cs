using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Soldier : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int dmg = 10;

    [SerializeField] private float angle;


    [field: SerializeField]
    public GameObject MainHand { get; set; }

    [field: SerializeField]
    public GameObject OffHand { get; set; }

    [field: SerializeField]
    public GameObject Target { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DebugLines();
    }

    public void Attack(GameObject target)
    {

    }

    private void DebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        Debug.DrawLine(OffHand.transform.position, OffHand.transform.position + OffHand.transform.forward, Color.blue);

        if (Target == null) return;

        Vector3 targetObjectPosition = new(Target.GetComponent<Soldier>().OffHand.transform.position.x, 0f, Target.GetComponent<Soldier>().OffHand.transform.position.z);
        Vector3 ownPosition = new(transform.position.x, 0f, transform.position.z);

        //Target Line
        Debug.DrawLine(transform.position, Target.transform.position, Color.red);

        //Angle Line
        Debug.DrawLine(ownPosition, targetObjectPosition, Color.yellow);
        angle = Mathf.Abs(Vector3.Angle(ownPosition, targetObjectPosition));
    }
}