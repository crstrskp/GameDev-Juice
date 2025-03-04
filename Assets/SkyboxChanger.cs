using System;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private List<Material> skyboxes = new List<Material>();
    private int counter = 0; 
    void OnEnable()
    {
        this.ChangeSkyBox(); 
    }

    private void OnDisable()
    {
        this.ChangeSkyBox();
    }

    private void ChangeSkyBox()
    {
        this.counter++;
        if (this.counter > skyboxes.Count - 1)
            this.counter = 0;
        RenderSettings.skybox = this.skyboxes[this.counter]; 
    }
}
