using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class RotatePhysical : MonoBehaviour
{

    [Header("かかる時間"), Range(0.0625f, 100)]
    public float duration = 1.0f;

    private Rigidbody rigidBody;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Unable to GetComponent at Awake");
        }
        if (rigidBody.isKinematic)
        {
            Debug.LogWarning("Rigidbody is Kinetic");
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.AddTorque(new Vector3(0, Mathf.PI*2 / duration * Time.fixedDeltaTime, 0), ForceMode.VelocityChange);
    }
}
