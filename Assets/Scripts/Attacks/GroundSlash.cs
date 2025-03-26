using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GroundSlash : MonoBehaviour
{
    [SerializeField] private float speed = 30f; 
    [SerializeField] private float slowDownRate = 0.01f;
    [SerializeField] private float detectingDistance = 0.1f;
    [SerializeField] private float destroyDelay = 5;

    public LayerMask targetLayerMask = new LayerMask();
    
    private Rigidbody rb;
    private bool stopped;
    private Collider collider; 


    private Vector3 originPos;

    private void ResetFx()
    {
        transform.position = originPos;
        StopAllCoroutines();
        rb.linearVelocity = Vector3.zero; 
        stopped = false; 
    }

    void OnEnable()
    {
        collider = GetComponentInChildren<MeshCollider>(); 
        originPos = transform.position; 
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            MoveForward();

           StartCoroutine(SlowDown());
        }
        else
        {
            Debug.Log("NO RB");
            Invoke(nameof(DisableFx), destroyDelay);
            // Destroy(gameObject, destroyDelay);
        }
    }
    
    void FixedUpdate()
    {
        if (!stopped)
        {
            HandleYPosition();
        }
    }

    private void MoveForward()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void HandleYPosition()
    {
        RaycastHit hit;
        Vector3 distance = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (Physics.Raycast(distance, transform.TransformDirection(-Vector3.up), out hit, detectingDistance))
        {
            transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
        Debug.DrawRay(distance, transform.TransformDirection(-Vector3.up * detectingDistance), Color.red);

    }
    
    IEnumerator SlowDown()
    {
        float t = 1;
        while (t > 0)
        { 
            rb.linearVelocity = Vector3.Lerp(Vector3.zero, rb.linearVelocity, t);
            t -= slowDownRate;
            yield return new WaitForSeconds(0.1f);
        }
        stopped = true;
        Invoke(nameof(DisableFx), destroyDelay);
    }
    
    void OnTriggerEnter(Collider other)
    {
            Debug.Log("H-HIT: " + other.name);
        
        if (targetLayerMask == other.gameObject.layer)
        {
        }
    }

    private void DisableFx() => gameObject.SetActive(false);
    private void OnDisable() => this.ResetFx();
}
