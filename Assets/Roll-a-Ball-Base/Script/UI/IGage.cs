﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollaBall2020.Gage
{
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
}
