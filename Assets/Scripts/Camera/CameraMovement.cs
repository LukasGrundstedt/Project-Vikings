using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;

    private float horizontal;
    private float vertical;

    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotateSpeed = 10;

    [SerializeField] private int zoomSpeed = 1;

    private float zoomTarget = 10f;
    private float currentZoom;
    private float zoomLerp;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        currentZoom = zoomTarget;
        zoomLerp = zoomTarget;
    }

    // Update is called once per frame
    void Update()
    {
        CameraTranslation();

        CameraRotation();

        CameraZoom();
    }

    private void CameraZoom()
    {
        zoomTarget += Input.mouseScrollDelta.y * zoomSpeed;
        zoomTarget = Mathf.Clamp(zoomTarget, 1f, 10f);

        currentZoom = zoomLerp;
        zoomLerp = Mathf.Lerp(currentZoom, zoomTarget, 0.05f);

        cam.transform.SetLocalPositionAndRotation(new Vector3(cam.transform.localPosition.x, zoomLerp, -zoomLerp), Quaternion.Euler(45f, 0f, 0f));
    }

    private void CameraRotation()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -rotateSpeed * Time.deltaTime, 0f);
        }
    }

    private void CameraTranslation()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        movement = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * movement;

        transform.position += movement.normalized * moveSpeed * Time.deltaTime;
    }
}
