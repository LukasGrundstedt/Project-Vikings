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

            switch (layer)
            {
                case 3: //Ground id
                    CurrentHitType = HitType.Ground;
                    break;
                case 6: //Interactable id
                    CurrentHitType = HitType.Object;
                    break;
                case 7: //Selectable id
                    CurrentHitType = HitType.Unit;
                    break;
                case 8: //Atackable id
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