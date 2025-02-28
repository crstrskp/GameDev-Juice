using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask interactables = new LayerMask();

    private AudioSource audioSource; 
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            var colliders = Physics.OverlapSphere(transform.position, 1.5f, interactables);
            foreach (var c in colliders)
            {
                var interactable = c.gameObject.GetComponent<Interactable>();
                interactable!.Interact();
                return; 
            }
            
            audioSource.Play();
        }
    }
}
