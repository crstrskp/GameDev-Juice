using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [Header("Dissolve Settings")]
    [SerializeField] [Range(0f, .1f)] private float dissolveDelay = 0.1f;
    [SerializeField] [Range(0f, .1f)] private float materializeDelay = .1f;
    [SerializeField] private float m_value;
    [SerializeField] private Renderer m_renderer;
    private Material m_dissolveMaterial;
    [SerializeField] private float m_incrementValue = .01f;


    void Awake()
    {
        m_dissolveMaterial = m_renderer.material;
        m_dissolveMaterial.SetFloat("Vector1_ea46cad2d2ec40cba83db77a61b462bf", m_value);       
        
    }
    private void Update()
    {
        m_dissolveMaterial.SetFloat("Vector1_ea46cad2d2ec40cba83db77a61b462bf", m_value); 
    }

    public void Dissolve()
    {
        // value from -1 to 1 then disable
        StartCoroutine(IncreaseDissolveValue());

    }

    private IEnumerator IncreaseDissolveValue()
    {
        while (m_value < 1f)
        {
            m_value += m_incrementValue;
            yield return new WaitForSeconds(dissolveDelay);
        }

        //gameObject.SetActive(false);
    }

    public void Materialize()
    {
        // enable then value from 1 to -1 
        StartCoroutine(DecreaseDissolveValue());
    }

    private IEnumerator DecreaseDissolveValue()
    {
        //gameObject.SetActive(true);

        while (m_value > -1f)
        {
            m_value -= m_incrementValue;
            yield return new WaitForSeconds(materializeDelay);
        }


    }
}
