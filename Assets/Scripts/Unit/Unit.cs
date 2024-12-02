using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{
    public GameObject CarriedMainObj { get; set; }
    public GameObject CarriedOffObj { get; set; }

    [SerializeField] private MainHand mainhand;
    [SerializeField] private OffHand offhand;

    // Start is called before the first frame update
    void Awake()
    {
        UnitManager.Instance.Units.Add(gameObject);
    }

    new private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (CarriedMainObj != null && Input.GetKeyUp(KeyCode.X) && Selected == true)
        { 
            DropAnyObject(CarriedMainObj);
        }
        else if (CarriedOffObj != null && Input.GetKeyUp(KeyCode.X) && Selected == true)
        {
            DropAnyObject(CarriedOffObj);
        }
    }

    private void DropAnyObject(GameObject obj)
    {
        obj.transform.SetParent(null, true);
        obj.layer = (int)Layer.Interactable;
        obj.transform.position = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);

        if (mainhand.MainHandObj.activeInHierarchy == false)
        {
            mainhand.MainHandObj.SetActive(true);
            CarriedMainObj = null;
            mainhand.MainHandFull = false;
        }
        else if (offhand.OffHandObj.activeInHierarchy == false)
        {
            offhand.OffHandObj.SetActive(true);
            CarriedOffObj = null;
            offhand.OffHandFull = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropArea"))
        {
            if (CarriedMainObj != null) 
            {
                DropAnyObject(CarriedMainObj);
            }

            if (CarriedOffObj != null)
            {
               DropAnyObject(CarriedOffObj);
            }
        }
    }

    public override void OnMouseDown()
    {
        UnitClick.UnitClicked(gameObject);
    }

    private void OnDestroy()
    {
        UnitManager.Instance.Units.Remove(gameObject);
    }
}