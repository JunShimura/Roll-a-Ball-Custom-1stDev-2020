using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

public class CatchGameObject : MonoBehaviour {

    public string targetTag;

    private List<GameObject> contactObject = new List<GameObject>();
    private Vector3 currentVelocity;
    public Vector3 fixedPosition;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        currentVelocity = transform.position - fixedPosition;
        fixedPosition = transform.position;
        if (contactObject.Capacity != 0) {
            contactObject.ForEach(
                delegate (GameObject gameObject) {
                    gameObject.transform.Translate(currentVelocity);
                });

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag) && !contactObject.Exists(  gameObject => gameObject.GetInstanceID() == collision.gameObject.GetInstanceID())) {
            contactObject.Add(collision.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag) && !contactObject.Exists(gameObject => gameObject.GetInstanceID() == collision.gameObject.GetInstanceID())) {
            contactObject.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag)) {
            int index = contactObject.FindIndex(gameObject => gameObject.GetInstanceID() == collision.gameObject.GetInstanceID());
            if (index != -1) {
                contactObject.RemoveAt(index);
            }
        }

    }
}
