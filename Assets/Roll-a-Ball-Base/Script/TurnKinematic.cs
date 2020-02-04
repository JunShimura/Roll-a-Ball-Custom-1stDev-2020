using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]

public class TurnKinematic : MonoBehaviour
{

    [Header("かかる時間"), Range(0.0625f, 30)]
    public float duration = 1.0f;
    [Header("移動する距離"), Range(0, 30)]
    [SerializeField] private float _distance;

    public float distance
    {
        set
        {
            _distance = value;
            targetPosition = GetTargetPosition(value);
        }
        get
        {
            return _distance;
        }

    }
    private Vector3 GetTargetPosition(float distance)
    {
        return transform.localPosition + transform.forward * distance;
    }


    float pastTime = 0;
    Vector3 homePosition;
    private Vector3 targetPosition;
    private Vector3 nextPosition;
    Rigidbody rigidBody;

    // Use this for initialization
    private void Awake()
    {
        homePosition = transform.position;
        targetPosition = GetTargetPosition(_distance);
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Unable to GetComponent at Awake");
        }
        if (!rigidBody.isKinematic)
        {
            Debug.LogWarning("Rigidbody is not Kinematic");
        }

    }

    private void FixedUpdate()
    {
        pastTime += Time.fixedDeltaTime;
        rigidBody.MovePosition(
            Vector3.Lerp(homePosition, targetPosition, Mathf.PingPong(pastTime, duration) / duration));
    }
}
