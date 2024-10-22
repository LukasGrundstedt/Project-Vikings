using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : Entity
{
    public GameObject CarriedMainObj { get; set; }
    public GameObject CarriedOffObj { get; set; }

    [SerializeField] private MainHand mainhand;
    [SerializeField] private OffHand offhand;

    // Start is called before the first frame update
    void Start()
    {
        UnitManager.Instance.Units.Add(gameObject);
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
        obj.layer = 6; //Interactable id
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

    private void OnDestroy()
    {
        UnitManager.Instance.Units.Remove(gameObject);
    }
}