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
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        Debug.DrawLine(OffHand.transform.position, OffHand.transform.position + OffHand.transform.forward, Color.blue);

        if (Target == null) return;
        Debug.DrawLine(transform.position, Target.transform.position, Color.red);

        Debug.DrawLine(transform.position, Target.GetComponent<Soldier>().OffHand.transform.position, Color.yellow);
        angle = Vector3.Angle(transform.position, Target.GetComponent<Soldier>().OffHand.transform.position);
    }

    public void Attack(GameObject target)
    {
    }
}