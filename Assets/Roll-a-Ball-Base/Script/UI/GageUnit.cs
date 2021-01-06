using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RollaBall2020.Gage
{
    public abstract class GageUnit:MonoBehaviour
    {
        private bool _state;
        public bool state
        {
            get {
                return _state;
            }
            set {
                if (_state != value) {
                    ChangeImage(value);
                    _state = value;
                }
            }
        }
        protected abstract void ChangeImage(bool v);
    }
}