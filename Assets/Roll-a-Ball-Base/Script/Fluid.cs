using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    [SerializeField]
    float mass = 1.0f;
    [SerializeField]
    float upForceRatio = 1.0f;
    [SerializeField]
    List<string> targetTag = new List<string>();
    [SerializeField]
    Vector3 force = new Vector3();
    Rigidbody targetRigidbody;

    private void SetForce(GameObject other)
    {
        targetRigidbody = other.GetComponent<Rigidbody>();
        force = new Vector3(0,targetRigidbody.mass / mass * upForceRatio,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            SetForce(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            SetForce(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            targetRigidbody = null;
            force = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce(force);
        }
    }
}
