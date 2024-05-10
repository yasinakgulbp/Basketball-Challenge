using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefab; // Oluþturulacak koyun prefabý
    public int sheepCount = 5; // Oluþturulacak koyun sayýsý
    public float spawnRadius = 10f; // Koyunlarýn spawn edileceði alanýn yarýçapý

    void Start()
    {
        SpawnSheep();
    }

    void SpawnSheep()
    {
        for (int i = 0; i < sheepCount; i++)
        {
            // Belirtilen alanda rastgele bir nokta seç
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Koyunu spawn et
            GameObject newSheep = Instantiate(sheepPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Belirtilen alanda rastgele bir nokta seç
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomPoint.x, 0f, randomPoint.y) + transform.position;
        return spawnPosition;
    }
}
