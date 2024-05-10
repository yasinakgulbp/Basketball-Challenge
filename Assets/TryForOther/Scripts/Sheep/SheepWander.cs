using UnityEngine;

public class SheepWander : MonoBehaviour
{
    public float wanderRadius = 5f; // Koyunun rastgele dolaþma yarýçapý
    public float wanderTimer = 2f; // Koyunun rastgele yönde hareket etme süresi
    public float moveSpeed = 2f; // Koyunun hareket hýzý
    public LayerMask obstacleMask; // Engel maskesi

    private Transform sheepTransform;
    private float timer;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private GameObject player; // Oyuncu objesi

    void Start()
    {
        sheepTransform = transform;
        timer = wanderTimer;
        startPosition = sheepTransform.position;
        targetPosition = startPosition;
    }

    void Update()
    {
        if (player == null)
            return; // Oyuncu atanmadýysa iþlem yapma

        // Oyuncudan kaç veya rastgele hareket et
        if (!isMoving)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                targetPosition = GetRandomPositionInRadius(startPosition, wanderRadius);
                timer = 0;
                isMoving = true;
            }
        }
        else
        {
            float distance = Vector3.Distance(sheepTransform.position, targetPosition);
            if (distance > 0.1f)
            {
                MoveTowards(targetPosition);
            }
            else
            {
                isMoving = false;
            }
        }
    }

    // Verilen merkez noktadan belirtilen yarýçapa kadar rastgele bir konum döndüren metod
    Vector3 GetRandomPositionInRadius(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;

        randomDirection.y = sheepTransform.position.y; // Koyunun yüksekliðini koru

        return randomDirection;
    }

    // Hedef konuma yavaþça hareket etmeyi saðlayan metod
    void MoveTowards(Vector3 targetPosition)
    {
        sheepTransform.position = Vector3.MoveTowards(sheepTransform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Oyuncu objesini koyuna atayan metod
    public void SetPlayer(GameObject playerObject)
    {
        player = playerObject;
    }
}
