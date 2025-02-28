using TMPro; 
using UnityEngine;

public class fxDemo : MonoBehaviour
{
    
    [SerializeField] private GameObject fxPrefab;
    
    public enum effectType { particle, vfxGraph, other }
    public effectType fxType;
    private TMP_Text label; 
    
    void Awake()
    {
        label = GetComponentInChildren<TMP_Text>();
        fxPrefab.SetActive(!fxPrefab.activeSelf);
        label.text = fxPrefab.name;
    }
    
    public void Play()
    {
        fxPrefab.SetActive(!fxPrefab.activeSelf);
    }
}
