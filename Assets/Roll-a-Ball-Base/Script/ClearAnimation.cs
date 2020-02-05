using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAnimation : MonoBehaviour {
    public float angleRate;
    public float frameRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ClearAnimationCoroutine());
    }
    private IEnumerator ClearAnimationCoroutine()
    {
        for (; ; )
        {
            transform.Rotate(Vector3.forward * angleRate);
            yield return new WaitForSeconds(1 / frameRate);
        }
    }
}
