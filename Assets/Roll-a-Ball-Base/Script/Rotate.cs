using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [Header("かかる時間"),Range(0.0625f,100)]
    public float duration = 1.0f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * 360/duration * Time.deltaTime);
		
	}
}
