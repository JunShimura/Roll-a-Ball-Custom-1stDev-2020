using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]ReadyCanvas readyCanvas;

    //Singlton
    static UIManager _S = null;
    private void Awake()
    {
        if (_S == null)
        {
            _S = this;
        }
        else
        {
            Debug.LogError("Duplicated GameController");
        }
    }
    static public UIManager GetInstance()
    {
        return _S;
    }
    public void SetReady()
    {
        readyCanvas.gameObject.SetActive(true);
    }
}
