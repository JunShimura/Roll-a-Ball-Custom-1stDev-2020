using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float upForce = 20.0f;
    public float animationFrameRate = 10f;
    public float animationRate = 10.0f;

    [SerializeField] GameObject brokenParticle;
    [SerializeField] float brokenTime = 0.5f;
    [SerializeField] GameObject gameController;

    private Vector3 force = new Vector3();
    private new Rigidbody rigidbody;
    private bool collisionFlag = false;
    private bool toJump = false;
    private bool isStarted = false;
    private List<int> contactGameObjectID = new List<int>();

    private void Reset()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().player = this.gameObject;
    }
    private void Start()
    {
        Reset();
        force = Vector3.zero;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        force.x = Input.GetAxis("Horizontal") * speed;
        force.z = Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Jump") && collisionFlag && !toJump) {
            toJump = true;
            force.y = upForce;
        }
        if (!isStarted && force.magnitude != 0.0f) {
            isStarted = true;
            gameController.GetComponent<GameController>().isStarted = true;
        }
    }

    void FixedUpdate()
    {
        if (force != Vector3.zero & rigidbody != null) {
            rigidbody.AddForce(force * speed);
            toJump = false;
            force.y = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionFlag = true;
        if (!contactGameObjectID.Contains(collision.gameObject.GetInstanceID())) {
            contactGameObjectID.Add(collision.gameObject.GetInstanceID());
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        collisionFlag = true;
        if (!contactGameObjectID.Contains(collision.gameObject.GetInstanceID())) {
            contactGameObjectID.Add(collision.gameObject.GetInstanceID());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (contactGameObjectID.Contains(collision.gameObject.GetInstanceID())) {
            contactGameObjectID.Remove(collision.gameObject.GetInstanceID());
        }
        if (contactGameObjectID.Count == 0) {
            collisionFlag = false;
        }
    }
    public void SetBroken()
    {
        //破壊された時の処理
        GameObject particleInstance = GameObject.Instantiate(brokenParticle, transform.position, Quaternion.identity);
        gameController.GetComponent<GameController>().ResetScene(brokenTime);
        Destroy(particleInstance, brokenTime);
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;
        Destroy(gameObject, brokenTime);
    }
    public void SetClear()
    {
        //レベルクリア時の処理
        rigidbody.isKinematic = true;
        transform.LookAt(GameObject.Find("Main Camera").transform);
        StartCoroutine(ClearAnimationCoroutine());
    }

    private IEnumerator ClearAnimationCoroutine()
    {
        for (; ; ) {
            transform.Rotate(Vector3.forward * animationRate);
            yield return new WaitForSeconds(1 / animationFrameRate);
        }

    }
}
