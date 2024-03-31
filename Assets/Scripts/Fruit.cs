using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole,sliced;
    Rigidbody fruitRigibody;
    Collider fruitCollider;

    private void Awake()
    {
        fruitRigibody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
    }
    void Slice(Vector3 direction, Vector3  position, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);
        fruitCollider.enabled = false;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody slice in slices)
        {
            slice.velocity = fruitRigibody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction,blade.transform.position,blade.sliceForce);
        }
    }
}
