using UnityEngine;

public class fxDemo : MonoBehaviour
{
    public enum effectType { particle, vfxGraph }
    public effectType fxType;

    [SerializeField] private GameObject fxPrefab; 

    void Awake()
    {
        fxPrefab.SetActive(false);
    }
    
    public void Play()
    {
        fxPrefab.SetActive(true);
    }
    
}
