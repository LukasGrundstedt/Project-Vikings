using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask rayCastable;

    public static HitType CurrentHitType { get; set; }

    private static RaycastHit hitInfo;
    public static RaycastHit HitInfo { get => hitInfo; private set => hitInfo = value; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetermineRayCastHit();
    }

    private void DetermineRayCastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 100f, rayCastable))
        {
            int layer = hitInfo.collider.gameObject.layer;

            switch ((Layer)layer)
            {
                case Layer.Ground:
                    CurrentHitType = HitType.Ground;
                    break;
                case Layer.UI:
                    CurrentHitType = HitType.UI;
                    break;
                case Layer.Interactable:
                    CurrentHitType = HitType.Object;
                    break;
                case Layer.Selectable:
                    CurrentHitType = HitType.Unit;
                    break;
                case Layer.Attackable:
                    CurrentHitType = HitType.Enemy;
                    break;
            }
        }
        else
        {
            //Acts as a null alternative
            CurrentHitType = HitType.None;
        }
    }
}