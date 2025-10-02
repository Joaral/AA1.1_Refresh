using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject targetFull;
    public GameObject targetBrokenPrefab;

    [Header("Config")]
    public string bulletTag = "Bullet";
    public float respawnTime = 3f;

    [SerializeField] private bool isBroken = false;

    private GameObject currentBrokenInstance;

    void Start()
    {
        targetFull.SetActive(true);
        if (currentBrokenInstance != null)
        {
            Destroy(currentBrokenInstance);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.name);
        if (other.CompareTag(bulletTag) && !isBroken)
        {
            Debug.Log("Objetivo alcanzado por una bala.");
            isBroken = true;

            targetFull.SetActive(false);

            currentBrokenInstance = Instantiate(targetBrokenPrefab,targetFull.transform.position, Quaternion.Euler(90f,0f,0f));
            Destroy(currentBrokenInstance, respawnTime);

            Invoke(nameof(Respawn), respawnTime);
        }
    }

    void Respawn()
    {
        targetFull.SetActive(true);
        isBroken = false;

    }
}
