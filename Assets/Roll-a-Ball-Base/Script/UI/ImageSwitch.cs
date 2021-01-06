using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageSwitch : MonoBehaviour, RollaBall2020.Gage.IGageUnit
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

    [SerializeField]
    Sprite image0ff;
    [SerializeField]
    Sprite imageOn;

    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    private void ChangeImage(bool state)
    {
        if (state) {
            image.sprite = imageOn;
        }
        else {
            image.sprite = image0ff;
        }
    }
}
