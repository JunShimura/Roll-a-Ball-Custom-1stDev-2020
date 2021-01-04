using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGage<T> 
{
    T Length
    {
        get;
        set;
    }
    T Value
    {
        get;
        set;
    }
}
