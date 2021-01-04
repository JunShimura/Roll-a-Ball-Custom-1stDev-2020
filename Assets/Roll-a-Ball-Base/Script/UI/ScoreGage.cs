using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]

public class ScoreGage : MonoBehaviour,IGage<int>
{
    private int _length = 0;
    public int Length
    {
        get
        {
            return _length;
        }
        set
        {
            _length = value;
        }
    }
    private int _value = 0;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    [SerializeField]
    GameObject gageUnit;
    void Awake()
    {

    }
    void OnEnable()
    {

    }
}
