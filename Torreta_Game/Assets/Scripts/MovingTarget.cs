using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [Header("Puntuación")]
    public int points = 100;
    public bool destroyOnHit = true;

    [Header("Targets")]
    public GameObject targetFull;
    public GameObject targetBrokenPrefab;

    [Header("Movimiento")]
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    private int currentWaypoint = 0;

    [Header("Config")]
    public string bulletTag = "Bullet";
    public float respawnTime = 3f;

    private bool isBroken = false;
    private GameObject currentBrokenInstance;

    void Start()
    {
        targetFull.SetActive(true);
        if (currentBrokenInstance != null)
        {
            Destroy(currentBrokenInstance);
        }
    }

    void Update()
    {
        MoveAlongWaypoints();
    }

    void MoveAlongWaypoints()
    {
        if (waypoints.Length == 0 || isBroken) return;

        Transform target = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0; // Vuelve al primero
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(bulletTag)) return;

        GameManager.instance.AddScore(points);

        if (destroyOnHit)
        {
            isBroken = true;
            targetFull.SetActive(false);

            currentBrokenInstance = Instantiate(
                targetBrokenPrefab,
                targetFull.transform.position,
                Quaternion.Euler(90f, 0f, 0f)
            );

            Destroy(currentBrokenInstance, respawnTime);
            Invoke(nameof(Respawn), respawnTime);
        }
        else
        {
            isBroken = false;
        }
    }

    void Respawn()
    {
        targetFull.SetActive(true);
        isBroken = false;
    }
}
