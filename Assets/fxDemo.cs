using UnityEngine;

public class fxDemo : MonoBehaviour
{
    public enum effectType { particle, vfxGraph }

    [SerializeField] private GameObject fxPrefab; 
    public effectType fxType;

    void Awake()
    {
        fxPrefab.SetActive(false);
    }
    
    public void Play()
    {
        fxPrefab.SetActive(true);
    }
    
}
