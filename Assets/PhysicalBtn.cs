using UnityEngine;

public class PhysicalBtn : MonoBehaviour, Interactable
{
    private AudioSource audioSource;
    private Animation anim; 
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }
    void ButtonPressed()
    {
        this.PlayAnim();
        this.PlaySound(); 
        GetComponentInParent<fxDemo>().Play();
    }

    public void Interact()
    {
        this.ButtonPressed();    
    }

    private void PlayAnim()
    {
        this.anim.Play();
    }

    private void PlaySound()
    {   
        this.audioSource.Play();
    }
    
    
}