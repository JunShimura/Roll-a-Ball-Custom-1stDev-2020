using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerLabel : MonoBehaviour {

    [SerializeField]
    GameObject gameControllerObject;
    [SerializeField]
    float resetSceneWait = 0.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            gameControllerObject.GetComponent<GameController>().ResetScene(resetSceneWait);

        }


    }
}
