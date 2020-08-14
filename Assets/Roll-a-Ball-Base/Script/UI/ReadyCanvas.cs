using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Canvas))]
public class ReadyCanvas : MonoBehaviour
{
    //Singlton
    static private ReadyCanvas _S = null;

    Canvas canvas;
    public delegate void Terminate();
    public Terminate terminate;

    private GameObject[] children;
    private bool activated = false;

    private void Awake()
    {
        if (_S == null)
        {
            _S = this;
        }
        else
        {
            Debug.LogError("Duplicated ReadyCanvas");
        }
        children = GetComponentsInChildren<Transform>()
            .Select(t => t.gameObject)
            .Where(o => o.GetInstanceID() != this.GetInstanceID())
            .ToArray();
        SetChildren(false);

    }
    public static ReadyCanvas GetInstance()
    {
        return _S;
    }
    public void SetActive()
    {
        SetChildren(true);
    }
    private void Update()
    {
        if (activated && Input.anyKeyDown)
        {
            terminate();
            SetChildren(false);
        }
    }
    private void SetChildren(bool active)
    {
        activated = active;
        foreach (GameObject go in children)
        {
            go.SetActive(active);
        }
    }
}
