using System;
using System.Collections.Generic;
using UnityEngine;

public class PostProcToggler : MonoBehaviour
{
    [SerializeField] private List<GameObject> postProcObjects = new List<GameObject>();
    private int counter;

    private void OnEnable()
    {
        this.TogglePostProc();
    }

    private void OnDisable()
    {
        this.TogglePostProc();
    }
    void TogglePostProc()
    {
        postProcObjects[this.counter].SetActive(false);
        this.counter++;
        if (this.counter > postProcObjects.Count - 1) 
            this.counter = 0;
        Debug.Log("counter: " + counter);
        postProcObjects[this.counter].SetActive(true);
        
    }
}
