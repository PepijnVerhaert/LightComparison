using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private string _rounding = "F3";

    [SerializeField] bool _range = false;

    public void ValueChanged(float value)
    {
        if(value <= 0f && !_range)
        {
            value = 0.001f;
        }

        _text.text = value.ToString(_rounding);
    }
}
