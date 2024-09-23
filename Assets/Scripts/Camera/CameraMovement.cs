using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;

    private float horizontal;
    private float vertical;

    [Header("Movement")]
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotateSpeed = 90;

    [Header("Zoomin")]
    [SerializeField] private float standardZoom = 10f;
    [Range(1, 10)]
    [SerializeField] private int zoomSpeed = 1;
    [SerializeField] private Vector2 zoomClamp = new Vector2(1f, 20f);

    private float zoomTarget;
    private float currentZoom;
    private float zoomLerp;
    private float closeupFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        zoomTarget = standardZoom;
        currentZoom = standardZoom;
        zoomLerp = standardZoom;
    }

    // Update is called once per frame
    void Update()
    {
        AdaptZoomSpeed();

        CameraTranslation(currentZoom);

        CameraRotation();

        CameraZoom();
    }

    private void CameraZoom()
    {
        zoomTarget += Input.mouseScrollDelta.y * zoomSpeed * closeupFactor;
        zoomTarget = Mathf.Clamp(zoomTarget, zoomClamp.x, zoomClamp.y);

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

    private void AdaptZoomSpeed()
    {
        closeupFactor = zoomTarget > 15f ? 1f : 0.2f;
    }

    private void CameraTranslation()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        movement = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * movement;

        transform.position += movement.normalized * moveSpeed * Time.deltaTime;
    }

    private void CameraTranslation(float zoomLevel)
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        movement = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * movement;

        transform.position += movement.normalized * moveSpeed * (zoomLevel * 0.2f) * Time.deltaTime;
    }
}
