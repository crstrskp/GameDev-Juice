using TMPro; 
using UnityEngine;

public class fxDemo : MonoBehaviour
{
    public enum effectType { particle, vfxGraph }
    public effectType fxType;
    private TMP_Text label; 
    
    [SerializeField] private GameObject fxPrefab; 

    void Awake()
    {
        label = GetComponentInChildren<TMP_Text>();
        fxPrefab.SetActive(false);
        label.text = fxPrefab.name;
    }
    
    public void Play()
    {
        fxPrefab.SetActive(true);
    }
    
}
