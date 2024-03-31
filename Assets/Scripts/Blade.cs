using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    Camera maincamera;
    Collider bladecollider;
    bool slicing;
    TrailRenderer bladetrail;

    public Vector3 direction { get; private set; }
    public float minSliceVelocity;
    public float sliceForce = 5f;
    private void Awake()
    {
        maincamera = Camera.main;
        bladecollider = GetComponent<Collider>();
        bladetrail = GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if(slicing)
        {
            ContinueSlicing();
        }
    }
    private void OnEnable()
    {
        StopSlicing();
    }
    private void OnDisable()
    {
        StopSlicing();
    }
    void StartSlicing()
    {
        Vector3 newPosition = maincamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;
        slicing = true;
        bladecollider.enabled = true;
        bladetrail.enabled = true;
        bladetrail.Clear();
    }
    void StopSlicing()
    {
        slicing = false;
        bladecollider.enabled = false;
        bladetrail.enabled = false;
    }
    void ContinueSlicing()
    {
        Vector3 newPosition = maincamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        direction = newPosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime; //velocity = distance divided by Time
        bladecollider.enabled = velocity > minSliceVelocity;
        transform.position = newPosition;
    }
}
