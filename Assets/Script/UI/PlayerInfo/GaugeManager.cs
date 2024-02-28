using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    public RectTransform gauge;
    public float maxValue = 100;
    public float currentValue = 100;
    float rate = 1;
    float gaugeWidth, gaugeHeight, zeroPoint;
    // Start is called before the first frame update
    void Start()
    {
        gaugeWidth = gauge.sizeDelta.x;
        gaugeHeight = gauge.sizeDelta.y;
        zeroPoint = gaugeHeight * (-165f / 400f);
    }

    // Update is called once per frame
    void Update()
    {
        rate = currentValue / maxValue;
        gauge.sizeDelta = new Vector2(gaugeWidth, gaugeHeight * rate);
        gauge.anchoredPosition = new Vector2(0, zeroPoint * (1 - rate));
    }

    public void SetMaxValue(float _floatValue)
    {
        maxValue = _floatValue;
    }
    public void SetCurrentValue(float _floatValue)
    {
        currentValue = _floatValue;
    }
}
