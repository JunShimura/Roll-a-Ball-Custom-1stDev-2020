using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Light))]

public class ItemInnerLight : MonoBehaviour {

    private new Light light;
    public float ratio=0.9f;

    // Use this for initialization
	void Start () {
        light = GetComponent<Light>();	
	}
	
	// Update is called once per frame
	void Update () {
        light.intensity *= ratio;
		
	}
}
