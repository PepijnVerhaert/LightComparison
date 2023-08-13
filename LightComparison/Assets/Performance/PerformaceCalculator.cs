using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PerformaceCalculator : MonoBehaviour
{
    [SerializeField] private TextChanger _MilazzoFPS;
    [SerializeField] private TextChanger _Grid4FPS;
    [SerializeField] private TextChanger _Grid8FPS;
    [SerializeField] private TextChanger _Grid16FPS;

    [SerializeField] private MilazzoAlgorithm _MilazzoAlgorithm;
    [SerializeField] private Grid4Algorithm _Grid4Algorithm;
    [SerializeField] private Grid8Algorithm _Grid8Algorithm;
    [SerializeField] private Grid16Algorithm _Grid16Algorithm;

    private float _timer = 0f;
    private float _updateTime = 1f;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _updateTime)
        {
            _timer -= _updateTime;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        _MilazzoFPS.ValueChanged(_MilazzoAlgorithm.GetFPS());
        _Grid4FPS.ValueChanged(_Grid4Algorithm.GetFPS());
        _Grid8FPS.ValueChanged(_Grid8Algorithm.GetFPS());
        _Grid16FPS.ValueChanged(_Grid16Algorithm.GetFPS());
    }
}
