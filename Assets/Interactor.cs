using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask interactables = new LayerMask();
    [SerializeField] private float radius = 2f;
    private AudioSource audioSource; 
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            var colliders = Physics.OverlapSphere(transform.position, radius, interactables);
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
