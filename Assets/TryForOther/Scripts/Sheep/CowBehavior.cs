using UnityEngine;

public class CowBehavior : MonoBehaviour
{
    public float movementSpeed = 3f; // Hareket hýzý
    public float rotationSpeed = 120f; // Dönme hýzý
    public float minIdleTime = 1f; // Minimum bekleme süresi
    public float maxIdleTime = 3f; // Maksimum bekleme süresi

    private GameObject cowBoss; // Boss koyun objesi
    private bool foundBoss = false; // Boss koyun bulundu mu?
    private bool isMoving = true; // Hareket ediyor mu?
    private float idleTimer = 0f; // Bekleme süresi
    private float currentIdleTime = 0f; // Þu anki bekleme süresi
    private Vector3 moveDirection;
    private Vector3 rotationAxis;
    private float rotationAngle;

    void Start()
    {
        cowBoss = null;
        SetRandomMoveDirection();
        SetRandomRotation();
        currentIdleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    void Update()
    {
        if (!foundBoss)
        {
            if (isMoving)
            {
                Move();
            }
            else
            {
                Rotate();
                idleTimer += Time.deltaTime;
                if (idleTimer >= currentIdleTime)
                {
                    isMoving = true;
                    idleTimer = 0f;
                    currentIdleTime = Random.Range(minIdleTime, maxIdleTime);
                }
            }
        }
        else
        {
            if (cowBoss != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, cowBoss.transform.position, movementSpeed * Time.deltaTime);
            }
        }
    }

    void Move()
    {
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        transform.Rotate(rotationAxis, rotationAngle * Time.deltaTime);
    }

    void SetRandomMoveDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void SetRandomRotation()
    {
        isMoving = false;
        rotationAxis = Vector3.up;
        rotationAngle = Random.Range(-rotationSpeed, rotationSpeed);
    }
}
