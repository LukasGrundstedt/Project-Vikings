using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject camObject;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        camObject = cam.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler
        (
            cam.transform.rotation.eulerAngles.x,
            camObject.transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z
        );
    }
}
