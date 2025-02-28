using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightController : MonoBehaviour
{
    [SerializeField]
    private Light m_light;
    [SerializeField]
    private int m_baseIntensity = 3;
    private int i = 0;

    void Update()
    {
        var data = 1 * Math.Sin((m_baseIntensity * Math.PI * 60 * i++ * 0.0004)) + (m_baseIntensity / 2);
        if (i > 2500) i = 0;
        
        m_light.intensity = Mathf.Lerp(m_light.intensity, (float)data, Time.deltaTime);
    }
}
