using UnityEngine;

public class BullerController : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * force, ForceMode.Impulse);

        Destroy(gameObject, 3f);
    }

    
}
