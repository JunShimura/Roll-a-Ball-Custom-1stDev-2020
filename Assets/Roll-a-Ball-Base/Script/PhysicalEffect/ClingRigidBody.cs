using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

///<summary>
///Cling by Moveposition the contacted GameObject contains RigidBody
/// </summary>
public class ClingRigidBody : MonoBehaviour
{
    [Header("Set target tags")]
    [Tooltip("List of tags of Clingable GameObject.If it's empty,target all gameobjects ")]
    public List<string> targetTag = new List<string>();

    /// <summary>
    /// Contacted Objects
    /// </summary>
    public class ContactObject
    {
        public Collision collision
        {
            get;
        }
        public Rigidbody rigidbody
        {
            get;
        }
        public int instanceID
        {
            get;
        }
        private Transform dummyTransform;
        private Vector3 prevPosition;
        private Vector3 prevFoward;
        private Vector3 adjustVector;
        private Quaternion adjustFoward;

        /// <summary>
        /// Constructor for ContactObject
        /// </summary>
        /// <param name="col">Contacted Collision</param>
        /// <param name="transform">transform to be engaged</param>
        public ContactObject(Collision col, Transform transform)
        {
            collision = col;
            rigidbody = collision.rigidbody;
            instanceID = collision.gameObject.GetInstanceID();
            dummyTransform = new GameObject().transform;
            dummyTransform.position = col.transform.position;
            dummyTransform.SetParent(transform);
            prevPosition = dummyTransform.position;
            prevFoward = dummyTransform.forward;
        }
        /// <summary>
        /// Move position to Engaged
        /// </summary>
        public void MoveRelativePosition()
        {

            if (rigidbody != null)
            {
                adjustVector = dummyTransform.position - prevPosition;
                if (adjustVector != Vector3.zero)
                {
                    rigidbody.MovePosition(rigidbody.gameObject.transform.position + adjustVector);
                    prevPosition = rigidbody.gameObject.transform.position + adjustVector;
                    dummyTransform.position = rigidbody.gameObject.transform.position + adjustVector;
                }
                adjustFoward = Quaternion.FromToRotation(prevFoward, dummyTransform.forward);
                if (adjustFoward != Quaternion.identity)
                {
                    rigidbody.MoveRotation(rigidbody.transform.rotation * adjustFoward);
                    prevFoward = dummyTransform.forward;
                }

            }
        }
        public void Remove()
        {
            Destroy(dummyTransform.gameObject);
        }
    }
    private List<ContactObject> contactObject = new List<ContactObject>();

    private Rigidbody catchRigidbody;

    // Use this for initialization
    void Awake()
    {
        catchRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CatchContact(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        CatchContact(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        int index = SearchContact(collision);
        if (index != -1)
        {
            contactObject[index].Remove();
            contactObject.RemoveAt(index);
        }
    }

    private void FixedUpdate()
    {
        if (contactObject.Capacity != 0)
        {
            contactObject.ForEach(
                delegate (ContactObject cO)
                {
                    if (cO.rigidbody != null)
                    {
                        cO.MoveRelativePosition();
                    }
                    else
                    {
                        contactObject.Remove(cO);
                    }
                }
             );
        }
    }


    /// <summary>
    /// Search colision object in ContactObject 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns>Found index</returns>
    private int SearchContact(Collision collision)
    {
        return contactObject.FindIndex(
            contactObject =>
                contactObject.instanceID == collision.gameObject.GetInstanceID());
    }

    /// <summary>
    /// Add collision object to the list of ContactObject
    /// </summary>
    /// <param name="collision">collision</param>
    private void CatchContact(Collision collision)
    {
        if (targetTag.Count == 0 || targetTag.Contains(collision.gameObject.tag))
        {
            if (SearchContact(collision) == -1)
            {
                contactObject.Add(new ContactObject(collision, this.transform));
            }
        }
    }
}
