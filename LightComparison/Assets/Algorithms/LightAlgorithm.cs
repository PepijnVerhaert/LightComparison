using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightAlgorithm : MonoBehaviour
{
    public abstract void CalculateLight(int x, int y, int rangeLimit);

    public abstract void ResetFPS();

    public abstract void SetMaps(MapScriptableObject map, ref List<List<Color>> colorMap);
}
