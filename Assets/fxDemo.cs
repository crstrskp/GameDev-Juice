using TMPro; 
using UnityEngine;

public class fxDemo : MonoBehaviour
{
    
    [SerializeField] private GameObject fxPrefab;
    
    public enum effectType { particle, vfxGraph, other }
    public effectType fxType;
    [Tooltip("If checked, the fx prefab will be both disabled and enabled on each press. If disabled, the fx will toggle on/off"), SerializeField] 
    private bool oneShot;
    private TMP_Text label; 
    
    void Awake()
    {
        label = GetComponentInChildren<TMP_Text>();
        fxPrefab.SetActive(!fxPrefab.activeSelf);
        label.text = fxPrefab.name;
    }
    
    public void Play()
    {
        if (this.oneShot)
        {
            fxPrefab.SetActive(false);
            fxPrefab.SetActive(true);
        }
        else
        {
            fxPrefab.SetActive(!fxPrefab.activeSelf);
        }
    }
}
