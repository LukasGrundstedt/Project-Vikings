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
        Debug.Log(rayCastable.value);   
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

            switch (layer)
            {
                case 3:
                    CurrentHitType = HitType.Ground;
                    break;
                case 6:
                    CurrentHitType = HitType.Object;
                    break;
                case 7:
                    CurrentHitType = HitType.Unit;
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