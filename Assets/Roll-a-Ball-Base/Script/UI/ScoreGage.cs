using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RollaBall2020.Gage;

[RequireComponent(typeof(Canvas))]

public class ScoreGage : MonoBehaviour, IGage<int>
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
            SetLength(value);
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
    List<GameObject> gageUnits = new List<GameObject>();
    private void SetLength(int l)
    {
        for (int i = 0; i < l; i++) {
            gageUnits.Add(Instantiate(gageUnit, this.gameObject.transform));
        }

    }

}
