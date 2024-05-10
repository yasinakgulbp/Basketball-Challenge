using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour
{
    public Transform player;
    public float approachDistance = 3f;
    public float runawayDistance = 5f;
    public float moveSpeed = 2f;
    public float maxSpeedMultiplier = 2f;
    public float accelerationTime = 1.5f;
    public float decelerationTime = 1f; // Yavaþlama süresi eklendi
    public LayerMask obstacleMask;

    private Vector3 initialPosition;
    private bool isPlayerNearby = false;
    private bool isRunningAway = false;
    private float currentSpeed = 0f;
    private float timeElapsed = 0f;
    private bool isDecelerating = false; // Koyunun yavaþlama durumu ekledi

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < approachDistance)
        {
            isPlayerNearby = true;

            timeElapsed += Time.deltaTime;
            currentSpeed = Mathf.Lerp(0f, moveSpeed * maxSpeedMultiplier, timeElapsed / accelerationTime);

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 targetPosition = transform.position - directionToPlayer * runawayDistance;

            if (!CheckObstacleInPath(targetPosition))
            {
                MoveTowards(targetPosition, currentSpeed);
                isRunningAway = true;
                isDecelerating = false; // Koyun kaçarken yavaþlama durumunu sýfýrla
            }
            else
            {
                Vector3 randomDirection = Random.insideUnitSphere * runawayDistance;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, runawayDistance, NavMesh.AllAreas);
                MoveTowards(hit.position, currentSpeed);
            }
        }
        else
        {
            if (!isDecelerating && isRunningAway) // Eðer koyun hala kaçýyorsa
            {
                timeElapsed = 0f;
                isDecelerating = true; // Koyunun yavaþlama durumunu aktif et
            }

            if (isDecelerating)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime / decelerationTime); // Yavaþça hýzý azalt
                MoveTowards(transform.position, currentSpeed);

                if (currentSpeed <= 0.01f)
                {
                    isPlayerNearby = false;
                    isRunningAway = false;
                    currentSpeed = 0f;
                    timeElapsed = 0f;
                    isDecelerating = false;
                }
            }
        }
    }

    void MoveTowards(Vector3 targetPosition, float speed)
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    bool CheckObstacleInPath(Vector3 targetPosition)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, targetPosition, out hit, obstacleMask))
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerNearby()
    {
        return isPlayerNearby;
    }

    public bool IsRunningAway()
    {
        return isRunningAway;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
