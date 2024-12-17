using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Soldier
{
    //[Header("Unit fields")]
    public GameObject CarriedMainObj { get; set; }
    public GameObject CarriedOffObj { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        UnitManager.Instance.Units.Add(gameObject);
    }

    new private void Start()
    {
        Setup();
        //EntityDisplay.DisplayStats(DisplayableHp(), DisplayableStats());
    }

    new void Update()
    {
        base.Update();

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

        if (MainHand.MainHandObj.activeInHierarchy == false)
        {
            MainHand.MainHandObj.SetActive(true);
            CarriedMainObj = null;
            MainHand.MainHandFull = false;
        }
        else if (OffHand.OffHandObj.activeInHierarchy == false)
        {
            OffHand.OffHandObj.SetActive(true);
            CarriedOffObj = null;
            OffHand.OffHandFull = false;
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

    protected override void Die()
    {
        GetComponent<Animation>().Play();
        OnDeath?.Invoke();
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